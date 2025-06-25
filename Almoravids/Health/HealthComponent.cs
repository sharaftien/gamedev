using Almoravids.Interfaces;

namespace Almoravids.Health
{
    public class HealthComponent : IHealth
    {
        private readonly int _maxHealth;
        private int _currentHealth;
        private bool _isInvulnerable;
        private float _invulnerabilityTimer;
        private Vector2 _knockbackVelocity;
        private float _knockbackTimer;
        private const float InvulnerabilityDuration = 3f; // 3 seconds
        private const float KnockbackDuration = 1f; // 0.3 seconds
        private const float KnockbackSpeed = 200f; // pixels per second

        public HealthComponent(int maxHealth = 3)
        {
            _maxHealth = maxHealth;
            _currentHealth = maxHealth;
            _isInvulnerable = false;
            _invulnerabilityTimer = 0f;
            _knockbackVelocity = Vector2.Zero;
            _knockbackTimer = 0f;
        }

        public int CurrentHealth => _currentHealth;
        public int MaxHealth => _maxHealth;
        public bool IsAlive => _currentHealth > 0;

        public Vector2 KnockbackVelocity => _knockbackVelocity; // expose for Hero

        public void TakeDamage(int damage, Vector2 knockbackDirection)
        {
            if (!_isInvulnerable)
            {
                _currentHealth = MathHelper.Clamp(_currentHealth - damage, 0, _maxHealth);
                if (IsAlive)
                {
                    _isInvulnerable = true;
                    _invulnerabilityTimer = InvulnerabilityDuration;
                    _knockbackVelocity = Vector2.Normalize(knockbackDirection) * KnockbackSpeed;
                    _knockbackTimer = KnockbackDuration;
                }
            }
        }

        public void Heal(int amount)
        {
            _currentHealth = MathHelper.Clamp(_currentHealth + amount, 0, _maxHealth);
            _isInvulnerable = false; // reset invulnreability
            _invulnerabilityTimer = 0f;
            _knockbackVelocity = Vector2.Zero; // reset knockback
            _knockbackTimer = 0f;
        }

        public void Update(GameTime gameTime)
        {
            if (_isInvulnerable)
            {
                _invulnerabilityTimer -= (float)gameTime.ElapsedGameTime.TotalSeconds;
                if (_invulnerabilityTimer <= 0f)
                {
                    _isInvulnerable = false;
                }
            }
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