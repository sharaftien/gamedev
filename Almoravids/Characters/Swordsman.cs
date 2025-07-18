
namespace Almoravids.Characters
{
    public class Swordsman : Enemy
    {
        private HealthComponent _healthComponent;
        private KnockbackComponent _knockbackComponent;

        public Swordsman(Texture2D texture, Vector2 startPosition, Hero target, string characterType = "swordsman", float speed = 80f)
            : base(texture, startPosition, target, characterType, speed)
        {
            _healthComponent = new HealthComponent(1);
            _knockbackComponent = new KnockbackComponent();
            CollisionComponent = new CollisionComponent(28f, 50f, 18f, 14f); 
        }       

        public override void Update(GameTime gameTime)
        {
            if (_healthComponent.IsAlive)
            {
                if (_knockbackComponent.KnockbackVelocity != Vector2.Zero)
                {
                    // knockback movement
                    MovementComponent.Position += _knockbackComponent.KnockbackVelocity * (float)gameTime.ElapsedGameTime.TotalSeconds;
                    MovementComponent.SetDirection(Vector2.Zero);
                    CollisionComponent.Update(MovementComponent.Position);
                }
                else
                {
                    base.Update(gameTime); // business as usual
                }
            }
            else
            {
                MovementComponent.SetDirection(Vector2.Zero);
            }

            _healthComponent.Update(gameTime);
            _knockbackComponent.Update(gameTime);
        }

        public HealthComponent HealthComponent => _healthComponent;
        public KnockbackComponent KnockbackComponent => _knockbackComponent;
    }
}