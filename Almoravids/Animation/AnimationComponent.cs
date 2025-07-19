
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

        public AnimationComponent(Texture2D texture, string characterType, float frameDuration = 0.1f)
        {
            _characterType = characterType; // store character type
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
                DefineSwordsmanAnimations(frameDuration);
            }

            _animatedSprite = new AnimatedSprite(_spriteSheet, characterType == "hero" ? "idle_down" : "walk_down");
            _currentDirection = Direction.Down;
            _currentAnimationName = characterType == "hero" ? "idle_down" : "walk_down";
            _lastMovingDirection = Direction.Down;
            _isDeathAnimationPlaying = false;
        }

        private void DefineHeroAnimations(float walkFrameDuration, float idleFrameDuration)
        {
            // define idle animations (2 frames per direction)
            DefineAnimations("idle", idleFrameDuration, new int[] { 61, 74, 87, 100 }, 2);

            // define walk animations (9 frames per direction)
            DefineAnimations("walk", walkFrameDuration, new int[] { 104, 117, 130, 143 }, 9);

            // define death animation (sprite 260-265)
            _spriteSheet.DefineAnimation("death", builder =>
            {
                builder.IsLooping(false); 
                for (int i = 260; i <= 265; i++)
                {
                    builder.AddFrame(regionIndex: i, duration: TimeSpan.FromSeconds(0.3f));
                }
            });
        }

        private void DefineSwordsmanAnimations(float frameDuration)
        {
            // define walk animations (9 frames per direction)
            DefineAnimations("walk", frameDuration, new int[] { 104, 117, 130, 143 }, 9);
        }

        private void DefineAnimations(string animationType, float frameDuration, int[] startIndices, int frameAmount, bool isLooping = true)
        {
            string[] directions = { "up", "left", "down", "right" };
            for (int i = 0; i < directions.Length; i++)
            {
                string direction = directions[i];
                int startIndex = startIndices[i];
                _spriteSheet.DefineAnimation($"{animationType}_{direction}", builder =>
                {
                    builder.IsLooping(isLooping);
                    for (int j = 0; j < frameAmount; j++)
                    {
                        builder.AddFrame(regionIndex: startIndex + j, duration: TimeSpan.FromSeconds(frameDuration));
                    }
                });
            }
        }

        public void Update(GameTime gameTime, Vector2 direction)
        {
            // skip other animations if death animation is playing
            if (_isDeathAnimationPlaying)
            {
                _animatedSprite.Update(gameTime);
                return;
            }

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
            string newAnimationName = $"walk_{_currentDirection.ToString().ToLower()}";
            if (!_isMoving && _characterType == "hero")
            {
                newAnimationName = $"idle_{_currentDirection.ToString().ToLower()}";
            }

            if (newAnimationName != _currentAnimationName)
            {
                _currentAnimationName = newAnimationName;
                _animatedSprite.SetAnimation(newAnimationName);
            }

            _animatedSprite.Update(gameTime);
        }

        public void SetAnimation(string animationName)
        {
            if (_currentAnimationName != animationName)
            {
                _currentAnimationName = animationName;
                _animatedSprite.SetAnimation(animationName);
                _isDeathAnimationPlaying = animationName == "death"; // check if death animation is playing
            }
        }

        public void Draw(SpriteBatch spriteBatch, Vector2 position, Color drawColor)
        {
            spriteBatch.Draw(_animatedSprite.TextureRegion.Texture, position, _animatedSprite.TextureRegion.Bounds, drawColor);
        }

        public void Draw(SpriteBatch spriteBatch, Vector2 position)
        {
            spriteBatch.Draw(_animatedSprite, position);
        }
    }
}