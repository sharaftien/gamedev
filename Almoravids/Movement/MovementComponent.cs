
namespace Almoravids.Movement
{
    public class MovementComponent
    {
        public Vector2 Position;
        public Vector2 Velocity { get; set; }
        public float Speed { get; set; }

        private Vector2 _direction;
        private readonly float _accelerationRate;
        private readonly float _decelerationRate;
        private readonly float _maxSpeed;
        private readonly bool _useAcceleration;
        private bool _isVelocityOverridden = false;

        public MovementComponent(Vector2 startPosition, float speed, float accelerationRate = 600f, float decelerationRate = 400f, float maxSpeed = 160f, bool useAcceleration = true)
        {
            Position = startPosition;
            Speed = speed;
            Velocity = Vector2.Zero;
            _direction = Vector2.Zero;
            _accelerationRate = accelerationRate;
            _decelerationRate = decelerationRate;
            _maxSpeed = maxSpeed;
            _useAcceleration = useAcceleration;
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

            if (_useAcceleration)
            {
                // Acceleratie en momentum van held
                if (_direction != Vector2.Zero)
                {
                    _direction.Normalize();
                    Velocity += _direction * _accelerationRate * deltaTime;

                    if (Velocity.Length() > _maxSpeed)
                    {
                        Velocity = Vector2.Normalize(Velocity) * _maxSpeed;
                    }
                }
                else
                {
                    if (Velocity.Length() > 0)
                    {
                        Vector2 deceleration = Vector2.Normalize(Velocity) * _decelerationRate * deltaTime;
                        if (deceleration.Length() > Velocity.Length())
                            Velocity = Vector2.Zero;
                        else
                            Velocity -= deceleration;
                    }
                }
            }
            else
            {
                // vaste snelheid voor enemies
                if (_direction != Vector2.Zero)
                {
                    _direction.Normalize();
                    Velocity = _direction * _maxSpeed;
                }
                else
                {
                    Velocity = Vector2.Zero;
                }
            }

            Position += Velocity * deltaTime;
        }
    }
}