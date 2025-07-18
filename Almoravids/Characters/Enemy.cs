
namespace Almoravids.Characters
{
    public abstract class Enemy : Character
    {
        protected Hero target; // tashfin

        public Enemy(Texture2D texture, Vector2 startPosition, Hero target, string characterType, float speed)
            : base(texture, startPosition, characterType, speed)
        {
            this.target = target;
        }

        public override void Update(GameTime gameTime)
        {
            // only move when visible
            if (!target.IsInvisible)
            {
                // Calculate the direction toward the target
                Vector2 direction = target.MovementComponent.Position - MovementComponent.Position;

                // Normalize direction to ensure consistent movement
                if (direction.Length() > 0)
                {
                    direction.Normalize();
                }
                MovementComponent.SetDirection(direction);
            }
            else
            {
                MovementComponent.SetDirection(Vector2.Zero); // else stop moving
            }
            base.Update(gameTime);
        }
    }
}