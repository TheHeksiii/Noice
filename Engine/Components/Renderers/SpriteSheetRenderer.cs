﻿
using Engine;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Scripts
{
    public class SpriteSheetRenderer : SpriteRenderer
    {
        private Vector2 spritesCount = new Vector2(1, 1);

        [ShowInEditor]
        public Vector2 SpritesCount
        {
            get => spritesCount;
            set
            {
                spritesCount = value;
                if (texture != null)
                {
                    SpriteSize = new Vector2(texture.Width / SpritesCount.X, texture.Height / SpritesCount.Y);
                }
            }
        }
        [ShowInEditor]
        public int MaxFrame { get; set; } = 1;

        [ShowInEditor]
        public float AnimationSpeed { get; set; } = 1;
        [ShowInEditor] public int CurrentSpriteIndex { get; set; }

        [ShowInEditor] public Vector2 SpriteSize { get; set; }
        public override void Start()
        {
        }
        public override void Update()
        {
            if ((int)Time.elapsedTicks % (int)(1 / MathHelper.Clamp(AnimationSpeed, 0.0001f, 2)) == 0)
            {

                if (CurrentSpriteIndex + 1 >= MaxFrame)
                {
                    CurrentSpriteIndex = 0;
                }
                else
                {
                    CurrentSpriteIndex++;
                }
            }
            base.Update();
        }
        public override void Draw(SpriteBatch batch)
        {
            if (GameObject == null || texture == null) { return; }
            batch.Draw(texture: texture,
                destinationRectangle: new Rectangle((int)transform.Position.X - (int)(transform.Anchor.X * SpriteSize.X * transform.Scale.Abs().X), (int)transform.Position.Y - (int)(transform.Anchor.Y * SpriteSize.Y * transform.Scale.Abs().X), (int)(SpriteSize.X * transform.Scale.Abs().X), (int)(SpriteSize.Y * transform.Scale.Abs().Y)),
                sourceRectangle: new Rectangle((int)SpriteSize.X * (int)(CurrentSpriteIndex % SpritesCount.X), (int)SpriteSize.Y * (int)(CurrentSpriteIndex / (SpritesCount.X)), (int)SpriteSize.X, (int)SpriteSize.Y),
                color: Color.White);// effects: RenderingHelpers.GetSpriteFlipEffects(transform), rotation: transform.Rotation
        }
        public override void OnTextureLoaded(Texture2D _texture, string _path)
        {
            SpriteSize = new Vector2(_texture.Width / SpritesCount.X, _texture.Height / SpritesCount.Y);

            base.OnTextureLoaded(_texture, _path);
        }
    }
}
