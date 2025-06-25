using Almoravids.Interfaces;

namespace Almoravids.Health
{
    public class HealthComponent : Interfaces.IHealth
    {
        private readonly int _maxHealth;
        private int _currentHealth;
        private bool _isInvulnerable;
        private float _invulnerabilityTimer;
        private const float InvulnerabilityDuration = 2f;

        public HealthComponent(int maxHealth = 3)
        {
            _maxHealth = maxHealth;
            _currentHealth = maxHealth;
            _isInvulnerable = false;
            _invulnerabilityTimer = 0f;
        }

        public int CurrentHealth => _currentHealth;
        public int MaxHealth => _maxHealth;
        public bool IsAlive => _currentHealth > 0;

        public void TakeDamage(int damage)
        {
            if (!_isInvulnerable)
            {
                _currentHealth = MathHelper.Clamp(_currentHealth - damage, 0, _maxHealth);
                if (IsAlive)
                {
                    _isInvulnerable = true;
                    _invulnerabilityTimer = InvulnerabilityDuration;
                }
            }
        }

        public void Heal(int amount)
        {
            _currentHealth = MathHelper.Clamp(_currentHealth + amount, 0, _maxHealth);
            _isInvulnerable = false; // reset invulnreability
            _invulnerabilityTimer = 0f;
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
        }
    }
}