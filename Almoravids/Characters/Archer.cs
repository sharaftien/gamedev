
namespace Almoravids.Characters
{
    public class Archer : Enemy
    {
        public Archer(Texture2D texture, Vector2 startPosition, Hero target, Texture2D questionTexture, ContentLoader contentLoader, string characterType = "archer", float speed = 0f)
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