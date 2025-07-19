
namespace Almoravids.Characters
{
    public enum Direction
    {
        Up, Down, Left, Right
    }

    public abstract class Character : IGameObject
    {
        public AnimationComponent AnimationComponent { get; set; }
        public MovementComponent MovementComponent { get; set; }
        public CollisionComponent CollisionComponent { get; set; }

        public Vector2 Position => MovementComponent.Position;

        public Character(Texture2D texture, Vector2 startPosition, string characterType, float speed, float accelerationRate = 600f, float decelerationRate = 400f, float maxSpeed = 120f, bool useAcceleration = true)
        {
            AnimationComponent = new AnimationComponent(texture, characterType);
            MovementComponent = new MovementComponent(startPosition, speed, accelerationRate, decelerationRate, maxSpeed, useAcceleration);
            CollisionComponent = new CollisionComponent(48, 48); // (tile size)
        }

        public abstract void Update(GameTime gameTime);

        public virtual void Draw(SpriteBatch spriteBatch)
        {
            AnimationComponent.Draw(spriteBatch, MovementComponent.Position);
        }

        public virtual void Reset(Vector2 startPosition)
        {
            MovementComponent.Position = startPosition;
        }
    }
}