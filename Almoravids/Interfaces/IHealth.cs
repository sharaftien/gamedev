
namespace Almoravids.Interfaces
{
    public interface IHealth
    {
        int CurrentHealth { get; }
        int MaxHealth { get; }
        bool IsAlive { get; }
        void TakeDamage(int damage, Vector2 knockbackDirection);
        void Update(GameTime gameTime); // invulnerability timer
    }
}
