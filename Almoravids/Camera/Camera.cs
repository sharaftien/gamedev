using Almoravids.Interfaces;

namespace Almoravids.Camera
{
    public class Camera : ICamera
    {
        public Vector2 Position { get; set; }
        private readonly float _mapWidth;
        private readonly float _mapHeight;
        private readonly float _screenWidth;
        private readonly float _screenHeight;
        private readonly float _deadZoneWidth;
        private readonly float _deadZoneHeight;

        public Camera(Vector2 initialPosition, float mapWidth = 1440f, float mapHeight = 960f, float screenWidth = 960f, float screenHeight = 720f, float deadZoneWidth = 200f, float deadZoneHeight = 150f)
        {
            Position = initialPosition; // start at target position
            _mapWidth = mapWidth; // 30 tiles * 48px
            _mapHeight = mapHeight; // 20 tiles * 48px
            _screenWidth = screenWidth; // 960px
            _screenHeight = screenHeight; //720px
            _deadZoneWidth = deadZoneWidth; // 200px horizontal (100px either direction off center)
            _deadZoneHeight = deadZoneHeight; // 150px vertical (75px either direction off center)
        }

        public void Update(Vector2 targetPosition, bool clampToMap = true)
        {
            // desired camera position to center target
            float centerX = Position.X;
            float centerY = Position.Y;

            // only move camera when outside the deadZone
            if (targetPosition.X < Position.X - _deadZoneWidth / 2)
                centerX = targetPosition.X + _deadZoneWidth / 2;
            else if (targetPosition.X > Position.X + _deadZoneWidth / 2)
                centerX = targetPosition.X - _deadZoneWidth / 2;

            if (targetPosition.Y < Position.Y - _deadZoneHeight / 2)
                centerY = targetPosition.Y + _deadZoneHeight / 2;
            else if (targetPosition.Y > Position.Y + _deadZoneHeight / 2)
                centerY = targetPosition.Y - _deadZoneHeight / 2;

            Position = new Vector2(centerX, centerY);

            if (clampToMap)
            {
                // clamp to map bounds to show entire map
                Position = new Vector2(
                    MathHelper.Clamp(Position.X, _screenWidth / 2, _mapWidth - _screenWidth / 2),
                    MathHelper.Clamp(Position.Y, _screenHeight / 2, _mapHeight - _screenHeight / 2)
                );
            }
        }

        public Matrix GetTransformMatrix()
        {
            // center camera on screen
            Vector2 snappedPosition = new Vector2((int)Position.X, (int)Position.Y);
            return Matrix.CreateTranslation(-snappedPosition.X + _screenWidth / 2, -snappedPosition.Y + _screenHeight / 2, 0);
        }
    }
}