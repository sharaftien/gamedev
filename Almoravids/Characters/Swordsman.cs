
namespace Almoravids.Characters
{
    public class Swordsman : Enemy
    {
        public Swordsman(Texture2D texture, Vector2 startPosition, Hero target, Texture2D questionTexture, string characterType = "swordsman", float speed = 80f)
            : base(texture, startPosition, target, questionTexture, characterType, speed)
        {
        }

        protected override void Attack(GameTime gameTime)
        {
            // calculate direction toward the target
            Vector2 direction = target.MovementComponent.Position - MovementComponent.Position;

            // normalize direction for consistent movement
            if (direction.Length() > 0)
            {
                direction.Normalize();
            }
            MovementComponent.SetDirection(direction);
        }
    }
}