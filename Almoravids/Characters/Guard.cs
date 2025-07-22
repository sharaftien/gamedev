
namespace Almoravids.Characters
{
    public class Guard : Enemy
    {
        private List<Vector2> _guardPath;
        private List<float> _waitTimes;
        private int _currentPathIndex;
        private const float PathThreshold = 5f;
        private float _waitTimer;
        private bool _movedFromSpawn; // check if guard has moved from spawn

        public Guard(Texture2D texture, Vector2 startPosition, Hero target, Texture2D questionTexture, string characterType, float speed, List<Vector2> guardPath, List<float> waitTimes = null)
            : base(texture, startPosition, target, questionTexture, characterType, speed)
        {
            _guardPath = guardPath != null && guardPath.Count > 0 ? guardPath : CreateDefaultPath(startPosition);
            _waitTimes = waitTimes != null && waitTimes.Count == _guardPath.Count ? waitTimes : Enumerable.Repeat(4f, _guardPath.Count).ToList();
            _currentPathIndex = 0;
            _waitTimer = 0f;
            _movedFromSpawn = false;
        }

        private List<Vector2> CreateDefaultPath(Vector2 start)
        {
            return new List<Vector2> { start, start + new Vector2(0, 200) };
        }

        protected override void Attack(GameTime gameTime)
        {
            if (!_guardPath.Any()) return;

            if (_waitTimer > 0)
            {
                _waitTimer -= (float)gameTime.ElapsedGameTime.TotalSeconds;
                MovementComponent.SetDirection(Vector2.Zero);
                return;
            }

            var currentTarget = _guardPath[_currentPathIndex];
            var direction = currentTarget - MovementComponent.Position;

            if (direction.Length() < PathThreshold)
            {
                // spawn move check
                if (_movedFromSpawn)
                {
                    _waitTimer = _waitTimes[_currentPathIndex];
                }

                _currentPathIndex = (_currentPathIndex + 1) % _guardPath.Count;
                direction = _guardPath[_currentPathIndex] - MovementComponent.Position;
                _movedFromSpawn = true; // moved from spawn check
            }

            if (direction != Vector2.Zero)
            {
                MovementComponent.SetDirection(Vector2.Normalize(direction));
            }
        }
    }
}