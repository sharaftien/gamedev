
namespace Almoravids.Movement
{
    public class MovementComponent
    {
        public Vector2 Position;
        public Vector2 Velocity { get; set; }
        public float Speed { get; set; }

        private Vector2 _direction;

        private const float AccelerationRate = 600f;
        private const float DecelerationRate = 400f;
        private const float MaxSpeed = 160f;

        private bool _isVelocityOverridden = false;

        public MovementComponent(Vector2 startPosition, float speed)
        {
            Position = startPosition;
            Speed = speed;
            Velocity = Vector2.Zero;
            _direction = Vector2.Zero;
        }

        public void SetDirection(Vector2 direction)
        {
            _direction = direction;
        }

        public void OverrideVelocity(Vector2 newVelocity)
        {
            Velocity = newVelocity;
            _isVelocityOverridden = true;
        }

        public void Update(GameTime gameTime)
        {
            float deltaTime = (float)gameTime.ElapsedGameTime.TotalSeconds;

            if (_isVelocityOverridden)
            {
                Position += Velocity * deltaTime;
                _isVelocityOverridden = false;
                return;
            }

            if (_direction != Vector2.Zero)
            {
                _direction.Normalize();
                Velocity += _direction * AccelerationRate * deltaTime;

                if (Velocity.Length() > MaxSpeed)
                {
                    Velocity = Vector2.Normalize(Velocity) * MaxSpeed;
                }
            }
            else
            {
                if (Velocity.Length() > 0)
                {
                    Vector2 deceleration = Vector2.Normalize(Velocity) * DecelerationRate * deltaTime;
                    if (deceleration.Length() > Velocity.Length())
                        Velocity = Vector2.Zero;
                    else
                        Velocity -= deceleration;
                }
            }

            Position += Velocity * deltaTime;
        }
    }
}