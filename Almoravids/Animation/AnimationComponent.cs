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
        private readonly SpriteSheet _walkSpriteSheet;
        private readonly SpriteSheet _idleSpriteSheet;
        private readonly AnimatedSprite _walkAnimatedSprite;
        private readonly AnimatedSprite _idleAnimatedSprite;
        private bool _isMoving;
        private Direction _currentDirection;
        private Direction _lastMovingDirection;
        private string _currentAnimationName;
        private AnimatedSprite _currentSprite;

        public AnimationComponent(Texture2D walkTexture, Texture2D idleTexture, string characterType, float frameDuration = 0.1f)
        {
            // maak texture atlas voor tashfin.png
            string atlasName = characterType == "hero" ? "Atlas/tashfin" : "Atlas/lamtuni";
            Texture2DAtlas walkAtlas = Texture2DAtlas.Create(atlasName, walkTexture, 64, 64);
            _walkSpriteSheet = new SpriteSheet($"SpriteSheet/{characterType}_walk", walkAtlas);

            if (characterType == "hero" && idleTexture != null)
            {
                // use separate idle atlas for hero
                Texture2DAtlas idleAtlas = Texture2DAtlas.Create("Atlas/tashfin_idle", idleTexture, 64, 64);
                _idleSpriteSheet = new SpriteSheet("SpriteSheet/tashfin_idle", idleAtlas);
                DefineHeroAnimations(frameDuration, idleFrameDuration: 0.2f);
                _idleAnimatedSprite = new AnimatedSprite(_idleSpriteSheet, "idle_down");
                _walkAnimatedSprite = new AnimatedSprite(_walkSpriteSheet, "walk_down");
                _currentSprite = _idleAnimatedSprite;
                _currentAnimationName = "idle_down";
            }
            else
            {
                // no idle animations for swordman
                _idleSpriteSheet = null;
                _idleAnimatedSprite = null;
                DefineSwordmanAnimations(frameDuration);
                _walkAnimatedSprite = new AnimatedSprite(_walkSpriteSheet, "walk_down");
                _currentSprite = _walkAnimatedSprite;
                _currentAnimationName = "walk_down";
            }

            _currentDirection = Direction.Down;
            _lastMovingDirection = Direction.Down;
        }

        private void DefineHeroAnimations(float walkFrameDuration, float idleFrameDuration)
        {
            // define idle animations (2 frames per direction)
            _idleSpriteSheet.DefineAnimation("idle_up", builder =>
            {
                builder.IsLooping(true)
                       .AddFrame(regionIndex: 0, duration: TimeSpan.FromSeconds(idleFrameDuration))
                       .AddFrame(1, duration: TimeSpan.FromSeconds(idleFrameDuration));
            });
            _idleSpriteSheet.DefineAnimation("idle_left", builder =>
            {
                builder.IsLooping(true)
                       .AddFrame(regionIndex: 2, duration: TimeSpan.FromSeconds(idleFrameDuration))
                       .AddFrame(3, duration: TimeSpan.FromSeconds(idleFrameDuration));
            });
            _idleSpriteSheet.DefineAnimation("idle_down", builder =>
            {
                builder.IsLooping(true)
                       .AddFrame(regionIndex: 4, duration: TimeSpan.FromSeconds(idleFrameDuration))
                       .AddFrame(5, duration: TimeSpan.FromSeconds(idleFrameDuration));
            });
            _idleSpriteSheet.DefineAnimation("idle_right", builder =>
            {
                builder.IsLooping(true)
                       .AddFrame(regionIndex: 6, duration: TimeSpan.FromSeconds(idleFrameDuration))
                       .AddFrame(7, duration: TimeSpan.FromSeconds(idleFrameDuration));
            });

            // define walk animations (9 frames per direction)
            _walkSpriteSheet.DefineAnimation("walk_up", builder =>
            {
                builder.IsLooping(true)
                       .AddFrame(regionIndex: 104, duration: TimeSpan.FromSeconds(walkFrameDuration))
                       .AddFrame(105, duration: TimeSpan.FromSeconds(walkFrameDuration))
                       .AddFrame(106, duration: TimeSpan.FromSeconds(walkFrameDuration))
                       .AddFrame(107, duration: TimeSpan.FromSeconds(walkFrameDuration))
                       .AddFrame(108, duration: TimeSpan.FromSeconds(walkFrameDuration))
                       .AddFrame(109, duration: TimeSpan.FromSeconds(walkFrameDuration))
                       .AddFrame(110, duration: TimeSpan.FromSeconds(walkFrameDuration))
                       .AddFrame(111, duration: TimeSpan.FromSeconds(walkFrameDuration))
                       .AddFrame(112, duration: TimeSpan.FromSeconds(walkFrameDuration));
            });
            _walkSpriteSheet.DefineAnimation("walk_left", builder =>
            {
                builder.IsLooping(true)
                       .AddFrame(regionIndex: 117, duration: TimeSpan.FromSeconds(walkFrameDuration))
                       .AddFrame(118, duration: TimeSpan.FromSeconds(walkFrameDuration))
                       .AddFrame(119, duration: TimeSpan.FromSeconds(walkFrameDuration))
                       .AddFrame(120, duration: TimeSpan.FromSeconds(walkFrameDuration))
                       .AddFrame(121, duration: TimeSpan.FromSeconds(walkFrameDuration))
                       .AddFrame(122, duration: TimeSpan.FromSeconds(walkFrameDuration))
                       .AddFrame(123, duration: TimeSpan.FromSeconds(walkFrameDuration))
                       .AddFrame(124, duration: TimeSpan.FromSeconds(walkFrameDuration))
                       .AddFrame(125, duration: TimeSpan.FromSeconds(walkFrameDuration));
            });
            _walkSpriteSheet.DefineAnimation("walk_down", builder =>
            {
                builder.IsLooping(true)
                       .AddFrame(regionIndex: 130, duration: TimeSpan.FromSeconds(walkFrameDuration))
                       .AddFrame(131, duration: TimeSpan.FromSeconds(walkFrameDuration))
                       .AddFrame(132, duration: TimeSpan.FromSeconds(walkFrameDuration))
                       .AddFrame(133, duration: TimeSpan.FromSeconds(walkFrameDuration))
                       .AddFrame(134, duration: TimeSpan.FromSeconds(walkFrameDuration))
                       .AddFrame(135, duration: TimeSpan.FromSeconds(walkFrameDuration))
                       .AddFrame(136, duration: TimeSpan.FromSeconds(walkFrameDuration))
                       .AddFrame(137, duration: TimeSpan.FromSeconds(walkFrameDuration))
                       .AddFrame(138, duration: TimeSpan.FromSeconds(walkFrameDuration));
            });
            _walkSpriteSheet.DefineAnimation("walk_right", builder =>
            {
                builder.IsLooping(true)
                       .AddFrame(regionIndex: 143, duration: TimeSpan.FromSeconds(walkFrameDuration))
                       .AddFrame(144, duration: TimeSpan.FromSeconds(walkFrameDuration))
                       .AddFrame(145, duration: TimeSpan.FromSeconds(walkFrameDuration))
                       .AddFrame(146, duration: TimeSpan.FromSeconds(walkFrameDuration))
                       .AddFrame(147, duration: TimeSpan.FromSeconds(walkFrameDuration))
                       .AddFrame(148, duration: TimeSpan.FromSeconds(walkFrameDuration))
                       .AddFrame(149, duration: TimeSpan.FromSeconds(walkFrameDuration))
                       .AddFrame(150, duration: TimeSpan.FromSeconds(walkFrameDuration))
                       .AddFrame(151, duration: TimeSpan.FromSeconds(walkFrameDuration));
            });
        }

        private void DefineSwordmanAnimations(float frameDuration)
        {
            // define walk animations (9 frames per direction, no idle animations)
            _walkSpriteSheet.DefineAnimation("walk_up", builder =>
            {
                builder.IsLooping(true)
                       .AddFrame(regionIndex: 104, duration: TimeSpan.FromSeconds(frameDuration))
                       .AddFrame(105, duration: TimeSpan.FromSeconds(frameDuration))
                       .AddFrame(106, duration: TimeSpan.FromSeconds(frameDuration))
                       .AddFrame(107, duration: TimeSpan.FromSeconds(frameDuration))
                       .AddFrame(108, duration: TimeSpan.FromSeconds(frameDuration))
                       .AddFrame(109, duration: TimeSpan.FromSeconds(frameDuration))
                       .AddFrame(110, duration: TimeSpan.FromSeconds(frameDuration))
                       .AddFrame(111, duration: TimeSpan.FromSeconds(frameDuration))
                       .AddFrame(112, duration: TimeSpan.FromSeconds(frameDuration));
            });
            _walkSpriteSheet.DefineAnimation("walk_left", builder =>
            {
                builder.IsLooping(true)
                       .AddFrame(regionIndex: 117, duration: TimeSpan.FromSeconds(frameDuration))
                       .AddFrame(118, duration: TimeSpan.FromSeconds(frameDuration))
                       .AddFrame(119, duration: TimeSpan.FromSeconds(frameDuration))
                       .AddFrame(120, duration: TimeSpan.FromSeconds(frameDuration))
                       .AddFrame(121, duration: TimeSpan.FromSeconds(frameDuration))
                       .AddFrame(122, duration: TimeSpan.FromSeconds(frameDuration))
                       .AddFrame(123, duration: TimeSpan.FromSeconds(frameDuration))
                       .AddFrame(124, duration: TimeSpan.FromSeconds(frameDuration))
                       .AddFrame(125, duration: TimeSpan.FromSeconds(frameDuration));
            });
            _walkSpriteSheet.DefineAnimation("walk_down", builder =>
            {
                builder.IsLooping(true)
                       .AddFrame(regionIndex: 130, duration: TimeSpan.FromSeconds(frameDuration))
                       .AddFrame(131, duration: TimeSpan.FromSeconds(frameDuration))
                       .AddFrame(132, duration: TimeSpan.FromSeconds(frameDuration))
                       .AddFrame(133, duration: TimeSpan.FromSeconds(frameDuration))
                       .AddFrame(134, duration: TimeSpan.FromSeconds(frameDuration))
                       .AddFrame(135, duration: TimeSpan.FromSeconds(frameDuration))
                       .AddFrame(136, duration: TimeSpan.FromSeconds(frameDuration))
                       .AddFrame(137, duration: TimeSpan.FromSeconds(frameDuration))
                       .AddFrame(138, duration: TimeSpan.FromSeconds(frameDuration));
            });
            _walkSpriteSheet.DefineAnimation("walk_right", builder =>
            {
                builder.IsLooping(true)
                       .AddFrame(regionIndex: 143, duration: TimeSpan.FromSeconds(frameDuration))
                       .AddFrame(144, duration: TimeSpan.FromSeconds(frameDuration))
                       .AddFrame(145, duration: TimeSpan.FromSeconds(frameDuration))
                       .AddFrame(146, duration: TimeSpan.FromSeconds(frameDuration))
                       .AddFrame(147, duration: TimeSpan.FromSeconds(frameDuration))
                       .AddFrame(148, duration: TimeSpan.FromSeconds(frameDuration))
                       .AddFrame(149, duration: TimeSpan.FromSeconds(frameDuration))
                       .AddFrame(150, duration: TimeSpan.FromSeconds(frameDuration))
                       .AddFrame(151, duration: TimeSpan.FromSeconds(frameDuration));
            });
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

            // only update last moving direction when actively moving
            if (_isMoving && newDirection != _currentDirection)
            {
                _lastMovingDirection = newDirection;
            }
            _currentDirection = newDirection;

            // choose animation
            string newAnimationName = _isMoving ? $"walk_{_currentDirection.ToString().ToLower()}" :
                (_idleSpriteSheet != null ? $"idle_{_lastMovingDirection.ToString().ToLower()}" : $"walk_{_lastMovingDirection.ToString().ToLower()}");
            if (newAnimationName != _currentAnimationName)
            {
                _currentAnimationName = newAnimationName;
                _currentSprite = newAnimationName.StartsWith("idle_") && _idleAnimatedSprite != null ? _idleAnimatedSprite : _walkAnimatedSprite;
                _currentSprite.SetAnimation(newAnimationName);
            }

            _currentSprite.Update(gameTime);
        }

        public void Draw(SpriteBatch spriteBatch, Vector2 position)
        {
            spriteBatch.Draw(_currentSprite, position);
        }
    }
}