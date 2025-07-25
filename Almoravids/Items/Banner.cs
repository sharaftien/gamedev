
namespace Almoravids.Items
{
    public class Banner : Item
    {
        private float _collisionTime;
        private const float RequiredCollisionTime = 1f;
        private readonly Texture2D _almoravidTexture;
        private bool _isCollected; // check if banner is collected

        public Banner(Texture2D texture, Vector2 position, ContentLoader contentLoader)
            : base(texture, position)
        {
            CollisionComponent = new CollisionComponent(48f, 96f, 0f, 0f); // 48x96 box, no offset
            _collisionTime = 0f; // initialize collision timer
            _almoravidTexture = contentLoader.LoadTexture2D("items/almoravidbanner"); // load almoravids texture
            _isCollected = false; // initialize as not collected
        }

        public override void Update(GameTime gameTime)
        {
            if (!_isActive)
                return;

            // update collision box position
            CollisionComponent.Update(_position);
        }

        public bool UpdateCollisionTimer(GameTime gameTime, Hero hero)
        {
            if (!_isActive)
                return false;

            if (CollisionComponent.BoundingBox.Intersects(hero.CollisionComponent.BoundingBox))
            {
                _collisionTime += (float)gameTime.ElapsedGameTime.TotalSeconds;
                if (_collisionTime >= RequiredCollisionTime)
                {
                    OnPickup(hero);
                    return true; // banner collected
                }
            }
            else
            {
                _collisionTime = 0f; // reset if collision stopped
            }
            return false;
        }

        public override void OnPickup(Hero hero)
        {
            _isActive = false;
            _isCollected = true;
            hero.AddBanner();
            Console.WriteLine($"Banner collected, total banners: {hero.BannerCount}");
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            if (_isActive)
            {
                spriteBatch.Draw(_texture, _position, Color.White);
            }
            else if (_isCollected)
            {
                spriteBatch.Draw(_almoravidTexture, _position, Color.White); // draw almoravidbanner
            }
        }
    }
}