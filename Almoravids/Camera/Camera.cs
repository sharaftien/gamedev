namespace Almoravids.Camera
{
    public class Camera
    {
        public Vector2 Position { get; set; }

        public Camera(Vector2 initialPosition)
        {
            Position = initialPosition;
        }

        public void Update(Vector2 targetPosition)
        {
            Position = targetPosition; // follow target
        }

        public Matrix GetTransformMatrix()
        {
            // center target (hero) on screen (1248/2, 960/2)
            return Matrix.CreateTranslation(-(Position.X - 624), -(Position.Y - 480), 0);
        }
    }
}