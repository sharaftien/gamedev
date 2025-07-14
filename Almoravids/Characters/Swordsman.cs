
namespace Almoravids.Characters
{
    public class Swordsman : Enemy
    {
        private HealthComponent _healthComponent;

        public Swordsman(Texture2D texture, Vector2 startPosition, Character target, string characterType = "swordsman", float speed = 80f)
            : base(texture, startPosition, target, characterType, speed)
        {
            _healthComponent = new HealthComponent(1);
            CollisionComponent = new CollisionComponent(28f, 50f, 18f, 14f); 
        }       

        public override void Update(GameTime gameTime)
        {
            if (_healthComponent.IsAlive)
            {
                base.Update(gameTime);
            }
            else
            {
                MovementComponent.SetDirection(Vector2.Zero);
            }
            _healthComponent.Update(gameTime);
        }

        public HealthComponent HealthComponent => _healthComponent;
    }
}