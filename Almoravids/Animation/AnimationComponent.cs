
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
        private bool _isDeathAnimationPlaying; // track death animation state
        private readonly string _characterType; // store character type
        private bool _isDeathAnimationFinished; // track if death animation has completed
        private float _deathAnimationTimer; // timer for death animation duration
        private const float DeathAnimationDuration = 1.8f; // 6 frames * 0.3s per frame

        public AnimationComponent(IAnimation animationSetup, string characterType)
        {
            _characterType = characterType; // store character type
            _animatedSprite = new AnimatedSprite(animationSetup.GetSpriteSheet(), characterType == "hero" ? "idle_down" : "walk_down");
            _currentAnimationName = characterType == "hero" ? "idle_down" : "walk_down";
            _currentDirection = Direction.Down;
            _lastMovingDirection = Direction.Down;
            _isDeathAnimationPlaying = false;
            _isDeathAnimationFinished = false;
            _deathAnimationTimer = 0f;
        }

        // Updates the animation based on movement and health
        public void Update(GameTime gameTime, Vector2 direction, bool isAlive)
        {
            // skip other animations if death animation is playing
            if (_isDeathAnimationPlaying || _isDeathAnimationFinished)
            {
                _animatedSprite.Update(gameTime);
                if (_isDeathAnimationPlaying)
                {
                    _deathAnimationTimer -= (float)gameTime.ElapsedGameTime.TotalSeconds;
                    if (_deathAnimationTimer <= 0f)
                    {
                        _isDeathAnimationPlaying = false;
                        _isDeathAnimationFinished = true;
                    }
                }
                return;
            }

            // if not alive start death animation
            if (!isAlive)
            {
                SetAnimation("death");
                _animatedSprite.Update(gameTime);
                return;
            }

            _isMoving = direction != Vector2.Zero;

            // update direction based on movement
            Direction newDirection = _currentDirection;
            if (_isMoving)
            {
                Vector2 normalizedDirection = direction;
                if (normalizedDirection != Vector2.Zero)
                {
                    normalizedDirection.Normalize();
                }

                float absX = Math.Abs(normalizedDirection.X);
                float absY = Math.Abs(normalizedDirection.Y);

                if (absX > absY + 0.1f) // if X is larger look sideways
                {
                    if (normalizedDirection.X > 0)
                    {
                        newDirection = Direction.Right;
                    }
                    else
                    {
                        newDirection = Direction.Left;
                    }
                }
                else if (absY > absX + 0.1f) // if Y is larger look up or down
                {
                    if (normalizedDirection.Y > 0)
                    {
                        newDirection = Direction.Down;
                    }
                    else
                    {
                        newDirection = Direction.Up;
                    }
                }
                else
                {
                    // else keep last moving direction
                    newDirection = _lastMovingDirection;
                }
            }

            // double look glitch fix
            if (_isMoving && newDirection != _currentDirection)
            {
                _lastMovingDirection = newDirection;
            }
            _currentDirection = newDirection;

            // choose animation
            string newAnimationName = $"walk_{_currentDirection.ToString().ToLower()}";
            if (!_isMoving && _characterType == "hero")
            {
                newAnimationName = $"idle_{_currentDirection.ToString().ToLower()}";
            }

            if (newAnimationName != _currentAnimationName)
            {
                SetAnimation(newAnimationName);
            }

            _animatedSprite.Update(gameTime);
        }

        public void SetAnimation(string animationName)
        {
            if (_currentAnimationName != animationName)
            {
                _currentAnimationName = animationName;
                _animatedSprite.SetAnimation(animationName);
                if (animationName == "death")
                {
                    _isDeathAnimationPlaying = true;
                    _isDeathAnimationFinished = false;
                    _deathAnimationTimer = DeathAnimationDuration;
                }
            }
        }

        public bool IsDeathAnimationFinished()
        {
            return _isDeathAnimationFinished;
        }

        // Resets the animation state
        public void Reset()
        {
            _isDeathAnimationPlaying = false;
            _isDeathAnimationFinished = false;
            _deathAnimationTimer = 0f;
            if (_characterType == "hero")
            {
                _currentAnimationName = "idle_down";
            }
            else
            {
                _currentAnimationName = "walk_down";
            }
            _animatedSprite.SetAnimation(_currentAnimationName);
        }

        // animation with custom color
        public void Draw(SpriteBatch spriteBatch, Vector2 position, Color drawColor)
        {
            spriteBatch.Draw(_animatedSprite.TextureRegion.Texture, position, _animatedSprite.TextureRegion.Bounds, drawColor);
        }

        // animation with default color
        public void Draw(SpriteBatch spriteBatch, Vector2 position)
        {
            spriteBatch.Draw(_animatedSprite, position);
        }
    }
}