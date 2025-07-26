using Almoravids.Interfaces;
using MonoGame.Extended.Graphics;

namespace Almoravids.Animation
{
    public abstract class CommonAnimation : IAnimation
    {
        protected Texture2D texture;
        protected string characterType;
        protected SpriteSheet spriteSheet;

        protected CommonAnimation(Texture2D texture, string characterType)
        {
            this.texture = texture;
            this.characterType = characterType;
            this.spriteSheet = CreateSpriteSheet();
        }

        public SpriteSheet GetSpriteSheet()
        {
            return spriteSheet;
        }

        public abstract void DefineAnimations();

        protected virtual SpriteSheet CreateSpriteSheet()
        {
            string atlasName = characterType == "hero" ? "Atlas/hero" : "Atlas/lamtuni";
            Texture2DAtlas atlas = Texture2DAtlas.Create(atlasName, texture, 64, 64);
            return new SpriteSheet($"SpriteSheet/{characterType}", atlas);
        }

        protected virtual void DefineCommonAnimations()
        {
            string[] directions = { "up", "left", "down", "right" };
            int[] walkStartIndices = { 104, 117, 130, 143 };
            for (int i = 0; i < directions.Length; i++)
            {
                string direction = directions[i];
                spriteSheet.DefineAnimation($"walk_{direction}", builder =>
                {
                    builder.IsLooping(true);
                    for (int j = 0; j < 9; j++)
                    {
                        builder.AddFrame(walkStartIndices[i] + j, TimeSpan.FromSeconds(0.1f));
                    }
                });
            }

            spriteSheet.DefineAnimation("death", builder =>
            {
                builder.IsLooping(false);
                for (int i = 260; i <= 265; i++)
                {
                    builder.AddFrame(i, TimeSpan.FromSeconds(0.3f));
                }
            });
        }
    }
}