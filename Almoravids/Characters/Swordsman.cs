
namespace Almoravids.Characters
{
    public class Swordsman : Enemy
    {
        public Swordsman(Texture2D texture, Vector2 startPosition, Character target, string characterType = "swordsman", float speed = 80f)
            : base(texture, startPosition, target, characterType, speed)
        {
            CollisionComponent = new CollisionComponent(28f, 50f, 18f, 14f); 
        }       

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
        }
    }
}