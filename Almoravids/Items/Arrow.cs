
namespace Almoravids.Items
{
    public class Arrow : IGameObject
    {
        private Texture2D _texture;
        private Vector2 _position;
        private Vector2 _velocity;
        private const float Speed = 250f; // arrow speed
        private bool _isActive;
        private CollisionComponent _collision;

        public Arrow(Texture2D texture, Vector2 startPosition, Vector2 direction)
        {
            _texture = texture;
            _position = startPosition;
            _velocity = Vector2.Normalize(direction) * Speed;
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
                spriteBatch.Draw(_texture, _position, Color.White);
        }

        public void Deactivate() => _isActive = false;
        public bool IsActive => _isActive;
        public Rectangle BoundingBox => _collision.BoundingBox;
    }
}