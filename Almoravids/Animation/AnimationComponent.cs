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
        private string _currentAnimationName;

        public AnimationComponent(Texture2D texture, float frameDuration = 0.15f)
        {
            // maak texture atlas voor tashfin.png
            Texture2DAtlas atlas = Texture2DAtlas.Create("Atlas/tashfin", texture, 64, 64);
            _spriteSheet = new SpriteSheet("SpriteSheet/tashfin", atlas);

            // definieer idle animaties (2 frames per richting)
            _spriteSheet.DefineAnimation("idle_up", builder =>
            {
                builder.IsLooping(true)
                       .AddFrame(regionIndex: 0, duration: TimeSpan.FromSeconds(frameDuration))
                       .AddFrame(1, duration: TimeSpan.FromSeconds(frameDuration));
            });
            _spriteSheet.DefineAnimation("idle_down", builder =>
            {
                builder.IsLooping(true)
                       .AddFrame(regionIndex: 32, duration: TimeSpan.FromSeconds(frameDuration))
                       .AddFrame(33, duration: TimeSpan.FromSeconds(frameDuration));
            });
            _spriteSheet.DefineAnimation("idle_left", builder =>
            {
                builder.IsLooping(true)
                       .AddFrame(regionIndex: 16, duration: TimeSpan.FromSeconds(frameDuration))
                       .AddFrame(17, duration: TimeSpan.FromSeconds(frameDuration));
            });
            _spriteSheet.DefineAnimation("idle_right", builder =>
            {
                builder.IsLooping(true)
                       .AddFrame(regionIndex: 48, duration: TimeSpan.FromSeconds(frameDuration))
                       .AddFrame(49, duration: TimeSpan.FromSeconds(frameDuration));
            });

            // definieer walk animaties (8 frames per richting)
            _spriteSheet.DefineAnimation("walk_up", builder =>
            {
                builder.IsLooping(true)
                       .AddFrame(regionIndex: 128, duration: TimeSpan.FromSeconds(frameDuration))
                       .AddFrame(129, duration: TimeSpan.FromSeconds(frameDuration))
                       .AddFrame(130, duration: TimeSpan.FromSeconds(frameDuration))
                       .AddFrame(131, duration: TimeSpan.FromSeconds(frameDuration))
                       .AddFrame(132, duration: TimeSpan.FromSeconds(frameDuration))
                       .AddFrame(133, duration: TimeSpan.FromSeconds(frameDuration))
                       .AddFrame(134, duration: TimeSpan.FromSeconds(frameDuration))
                       .AddFrame(135, duration: TimeSpan.FromSeconds(frameDuration));
            });
            _spriteSheet.DefineAnimation("walk_down", builder =>
            {
                builder.IsLooping(true)
                       .AddFrame(regionIndex: 160, duration: TimeSpan.FromSeconds(frameDuration))
                       .AddFrame(161, duration: TimeSpan.FromSeconds(frameDuration))
                       .AddFrame(162, duration: TimeSpan.FromSeconds(frameDuration))
                       .AddFrame(163, duration: TimeSpan.FromSeconds(frameDuration))
                       .AddFrame(164, duration: TimeSpan.FromSeconds(frameDuration))
                       .AddFrame(165, duration: TimeSpan.FromSeconds(frameDuration))
                       .AddFrame(166, duration: TimeSpan.FromSeconds(frameDuration))
                       .AddFrame(167, duration: TimeSpan.FromSeconds(frameDuration));
            });
            _spriteSheet.DefineAnimation("walk_left", builder =>
            {
                builder.IsLooping(true)
                       .AddFrame(regionIndex: 144, duration: TimeSpan.FromSeconds(frameDuration))
                       .AddFrame(145, duration: TimeSpan.FromSeconds(frameDuration))
                       .AddFrame(146, duration: TimeSpan.FromSeconds(frameDuration))
                       .AddFrame(147, duration: TimeSpan.FromSeconds(frameDuration))
                       .AddFrame(148, duration: TimeSpan.FromSeconds(frameDuration))
                       .AddFrame(149, duration: TimeSpan.FromSeconds(frameDuration))
                       .AddFrame(150, duration: TimeSpan.FromSeconds(frameDuration))
                       .AddFrame(151, duration: TimeSpan.FromSeconds(frameDuration));
            });
            _spriteSheet.DefineAnimation("walk_right", builder =>
            {
                builder.IsLooping(true)
                       .AddFrame(regionIndex: 176, duration: TimeSpan.FromSeconds(frameDuration))
                       .AddFrame(177, duration: TimeSpan.FromSeconds(frameDuration))
                       .AddFrame(178, duration: TimeSpan.FromSeconds(frameDuration))
                       .AddFrame(179, duration: TimeSpan.FromSeconds(frameDuration))
                       .AddFrame(180, duration: TimeSpan.FromSeconds(frameDuration))
                       .AddFrame(181, duration: TimeSpan.FromSeconds(frameDuration))
                       .AddFrame(182, duration: TimeSpan.FromSeconds(frameDuration))
                       .AddFrame(183, duration: TimeSpan.FromSeconds(frameDuration));
            });

            _animatedSprite = new AnimatedSprite(_spriteSheet, "idle_down");
            _currentDirection = Direction.Down;
            _currentAnimationName = "idle_down";
        }

        public void Update(GameTime gameTime, Vector2 direction)
        {
            _isMoving = direction != Vector2.Zero;

            // update richting gebaseerd op beweging
            if (direction.X > 0) _currentDirection = Direction.Right;
            else if (direction.X < 0) _currentDirection = Direction.Left;
            else if (direction.Y > 0) _currentDirection = Direction.Down;
            else if (direction.Y < 0) _currentDirection = Direction.Up;

            // kies animatie
            string newAnimationName = _isMoving ? $"walk_{_currentDirection.ToString().ToLower()}" : $"idle_{_currentDirection.ToString().ToLower()}";
            if (newAnimationName != _currentAnimationName)
            {
                _currentAnimationName = newAnimationName;
                _animatedSprite.SetAnimation(_currentAnimationName);
            }

            _animatedSprite.Update(gameTime);
        }

        public void Draw(SpriteBatch spriteBatch, Vector2 position)
        {
            spriteBatch.Draw(_animatedSprite, position);
        }
    }
}