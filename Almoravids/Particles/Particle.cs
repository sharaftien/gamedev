
namespace Almoravids.Particles
{
    public class Particle
    {
        public Vector2 Position;
        public Vector2 Velocity;
        public float Life;
        public Color Color;

        public Particle(Vector2 position, Vector2 velocity, float life, Color color)
        {
            Position = position;
            Velocity = velocity;
            Life = life;
            Color = color;
        }

        public void Update(GameTime gameTime)
        {
            Position += Velocity * (float)gameTime.ElapsedGameTime.TotalSeconds;
            Life -= (float)gameTime.ElapsedGameTime.TotalSeconds;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            if (Life > 0f)
                spriteBatch.Draw(Game1.Pixel, new Rectangle((int)Position.X, (int)Position.Y, 2, 2), Color);
        }

        public bool IsDead => Life <= 0f;
    }
}
