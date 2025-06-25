
namespace Almoravids.Movement
{
    public class MovementComponent
    {
        public Vector2 Position { get; set; }
        public Vector2 Velocity { get; set; }
        public float Speed { get; set; }

        public MovementComponent(Vector2 startPosition, float speed)
        {
            Position = startPosition;
            Speed = speed;
            Velocity = Vector2.Zero;
        }

        public void Update(GameTime gameTime)
        {
            Position += Velocity * (float)gameTime.ElapsedGameTime.TotalSeconds;
        }

        public void SetDirection(Vector2 direction)
        {
            if (direction != Vector2.Zero)
            {
                direction.Normalize();
            }
            Velocity = direction * Speed;
        }
    }
}