
namespace Almoravids.Characters
{
    public class Guard : Enemy
    {
        private List<Vector2> _guardPath;
        private int _currentPathIndex;
        private const float PathThreshold = 5f;

        public Guard(Texture2D texture, Vector2 startPosition, Hero target, Texture2D questionTexture, string characterType, float speed, List<Vector2> guardPath)
            : base(texture, startPosition, target, questionTexture, characterType, speed)
        {
            _guardPath = guardPath ?? CreateDefaultPath(startPosition);
            _currentPathIndex = 0;
        }

        private List<Vector2> CreateDefaultPath(Vector2 start)
        {
            return new List<Vector2> { start, start + new Vector2(0, 200) };
        }

        protected override void Attack(GameTime gameTime)
        {
            if (!_guardPath.Any()) return;

            var currentTarget = _guardPath[_currentPathIndex];
            var direction = currentTarget - MovementComponent.Position;

            if (direction.Length() < PathThreshold)
            {
                _currentPathIndex = (_currentPathIndex + 1) % _guardPath.Count;
                direction = _guardPath[_currentPathIndex] - MovementComponent.Position;
            }

            if (direction != Vector2.Zero)
            {
                MovementComponent.SetDirection(Vector2.Normalize(direction));
            }
        }
    }
}