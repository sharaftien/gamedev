
namespace Almoravids.Health
{
    public class KnockbackComponent
    {
        private Vector2 _knockbackVelocity;
        private float _knockbackTimer;
        private const float KnockbackDuration = 1f; // 0.3 seconds
        private const float KnockbackSpeed = 200f; // pixels per second

        public KnockbackComponent()
        {
            _knockbackVelocity = Vector2.Zero;
            _knockbackTimer = 0f;
        }

        public Vector2 KnockbackVelocity => _knockbackVelocity; // expose for Hero

        public void ApplyKnockback(Vector2 knockbackDirection)
        {
            if (knockbackDirection != Vector2.Zero)
            {
                _knockbackVelocity = Vector2.Normalize(knockbackDirection) * KnockbackSpeed;
                _knockbackTimer = KnockbackDuration;
            }
        }

        public void Update(GameTime gameTime)
        {
            if (_knockbackTimer > 0f)
            {
                _knockbackTimer -= (float)gameTime.ElapsedGameTime.TotalSeconds;
                if (_knockbackTimer <= 0f)
                {
                    _knockbackVelocity = Vector2.Zero;
                }
            }
        }
    }
}