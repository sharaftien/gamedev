using Almoravids.Interfaces;

namespace Almoravids.Health
{
    public class HealthComponent : Interfaces.IHealth
    {
        private readonly int _maxHealth;
        private int _currentHealth;

        public HealthComponent(int maxHealth = 3)
        {
            _maxHealth = maxHealth;
            _currentHealth = maxHealth;
        }

        public int CurrentHealth => _currentHealth;
        public int MaxHealth => _maxHealth;
        public bool IsAlive => _currentHealth > 0;

        public void TakeDamage(int damage)
        {
            _currentHealth = MathHelper.Clamp(_currentHealth - damage, 0, _maxHealth);
        }

        public void Heal(int amount)
        {
            _currentHealth = MathHelper.Clamp(_currentHealth + amount, 0, _maxHealth);
        }
    }
}