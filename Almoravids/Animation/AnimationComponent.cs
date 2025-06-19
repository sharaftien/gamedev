using Almoravids;
using Almoravids.Characters;
using MonoGame.Extended.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Almoravids.Animation
{
    public class AnimationComponent
    {
        private readonly SpriteSheet _spriteSheet;
        private readonly AnimatedSprite _animatedSprite;
        private bool _isMoving;
        private Direction _currentDirection;
        private Direction _lastMovingDirection;
        private string _currentAnimationName;

        public AnimationComponent(Texture2D texture, string characterType, float frameDuration = 0.1f)
        {
            // maak texture atlas voor tashfin.png
            string atlasName = characterType == "hero" ? "Atlas/tashfin" : "Atlas/lamtuni";
            Texture2DAtlas atlas = Texture2DAtlas.Create(atlasName, texture, 64, 64);
            _spriteSheet = new SpriteSheet($"SpriteSheet/{characterType}", atlas);

            if (characterType == "hero")
            {
                DefineHeroAnimations(frameDuration, idleFrameDuration: 0.2f);
            }
            else
            {
                DefineSwordmanAnimations(frameDuration);
            }

            _animatedSprite = new AnimatedSprite(_spriteSheet, characterType == "hero" ? "idle_down" : "walk_down");
            _currentDirection = Direction.Down;
            _currentAnimationName = characterType == "hero" ? "idle_down" : "walk_down";
            _lastMovingDirection = Direction.Down;
        }

        private void DefineHeroAnimations(float walkFrameDuration, float idleFrameDuration)
        {
            // define idle animations (2 frames per direction)
            DefineIdleAnimations(_spriteSheet, idleFrameDuration, new int[] { 48, 74, 87, 100 });

            // define walk animations (9 frames per direction)
            DefineWalkAnimations(_spriteSheet, walkFrameDuration);
        }

        private void DefineSwordmanAnimations(float frameDuration)
        {
            // define walk animations (9 frames per direction)
            DefineWalkAnimations(_spriteSheet, frameDuration);
        }

        private void DefineIdleAnimations(SpriteSheet sheet, float frameDuration, int[] startIndexPerDirection)
        {
            string[] directions = { "up", "left", "down", "right" };
            for (int i = 0; i < directions.Length; i++)
            {
                string direction = directions[i];
                int startIndex = startIndexPerDirection[i];
                sheet.DefineAnimation($"idle_{direction}", builder =>
                {
                    builder.IsLooping(true);
                    for (int j = 0; j < 2; j++)
                    {
                        builder.AddFrame(regionIndex: startIndex + j, duration: TimeSpan.FromSeconds(frameDuration));
                    }
                });
            }
        }

        private void DefineWalkAnimations(SpriteSheet sheet, float frameDuration)
        {
            string[] directions = { "up", "left", "down", "right" };
            int[] startIndices = { 104, 117, 130, 143 }; // firsts index of each walk animation
            for (int i = 0; i < directions.Length; i++)
            {
                string direction = directions[i];
                int startIndex = startIndices[i];
                sheet.DefineAnimation($"walk_{direction}", builder =>
                {
                    builder.IsLooping(true);
                    for (int j = 0; j < 9; j++)
                    {
                        builder.AddFrame(regionIndex: startIndex + j, duration: TimeSpan.FromSeconds(frameDuration));
                    }
                });
            }
        }

        public void Update(GameTime gameTime, Vector2 direction)
        {
            _isMoving = direction != Vector2.Zero;

            // update direction based on movement
            Direction newDirection = _currentDirection;
            if (_isMoving)
            {
                if (direction.X > 0) newDirection = Direction.Right;
                else if (direction.X < 0) newDirection = Direction.Left;
                else if (direction.Y > 0) newDirection = Direction.Down;
                else if (direction.Y < 0) newDirection = Direction.Up;
            }

            // double look glitch oplossing
            if (_isMoving && newDirection != _currentDirection)
            {
                _lastMovingDirection = newDirection;
            }
            _currentDirection = newDirection;

            // choose animation
            string newAnimationName = _isMoving ? $"walk_{_currentDirection.ToString().ToLower()}" :
                (_spriteSheet.GetAnimation("idle_down") != null ? $"idle_{_lastMovingDirection.ToString().ToLower()}" : $"walk_{_lastMovingDirection.ToString().ToLower()}");
            if (newAnimationName != _currentAnimationName)
            {
                _currentAnimationName = newAnimationName;
                _animatedSprite.SetAnimation(newAnimationName);
            }

            _animatedSprite.Update(gameTime);
        }

        public void Draw(SpriteBatch spriteBatch, Vector2 position)
        {
            spriteBatch.Draw(_animatedSprite, position);
        }
    }
}