
namespace Almoravids.Items
{
    public abstract class Item : IGameObject
    {
        public CollisionComponent CollisionComponent { get; protected set; }
        protected readonly Texture2D _texture;
        protected Vector2 _position;
        protected bool _isActive;
        public Vector2 Position => _position; // expose position (banner)

        protected Item(Texture2D texture, Vector2 position)
        {
            _texture = texture;
            _position = position;
            _isActive = true;
            CollisionComponent = new CollisionComponent(32f, 32f, 8f, 8f); // 32x32 box met 8px offset
            CollisionComponent.Update(_position); // initialize collision box
        }

        public virtual void Update(GameTime gameTime)
        {
        }

        public virtual void Draw(SpriteBatch spriteBatch)
        {
            if (_isActive)
            {
                spriteBatch.Draw(_texture, _position, Color.White);
            }
        }

        public bool IsActive => _isActive;

        public virtual void OnPickup(Hero hero)
        {
            _isActive = false; // deactivate after pickup
        }

        // Apply item effect during collision with enemy
        public virtual void ApplyEffect(Hero hero, Enemy enemy)
        {
        }
    }
}