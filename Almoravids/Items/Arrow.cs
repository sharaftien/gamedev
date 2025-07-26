using Almoravids.Interfaces;
using Almoravids.Collision;

namespace Almoravids.Items
{
    public class Arrow : IGameObject
    {
        private Texture2D _texture;
        private Vector2 _position;
        private Vector2 _velocity;
        private float _rotation;
        private const float Speed = 250f; // arrow speed
        private bool _isActive;
        private CollisionComponent _collision;

        public Arrow(Texture2D texture, Vector2 startPosition, Vector2 direction)
        {
            _texture = texture;
            _position = startPosition;
            _velocity = Vector2.Normalize(direction) * Speed;
            _rotation = (float)Math.Atan2(direction.Y, direction.X); // calculate rotation
            _isActive = true;

            _collision = new CollisionComponent(32f, 5f, 0f, 14f); // horizontal box (whitespace fix)
            _collision.Update(_position);
        }

        public void Update(GameTime gameTime)
        {
            if (!_isActive) return;

            _position += _velocity * (float)gameTime.ElapsedGameTime.TotalSeconds;
            _collision.Update(_position);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            if (_isActive)
            {
                Vector2 origin = new Vector2(_texture.Width / 2f, _texture.Height / 2f); // get center to rotate correctly
                spriteBatch.Draw(_texture, _position, null, Color.White, _rotation, origin, 1f, SpriteEffects.None, 0f);
            }
        }

        public void Deactivate() => _isActive = false;
        public bool IsActive => _isActive;
        public Rectangle BoundingBox => _collision.BoundingBox;
    }
}