using Almoravids.Items;
using Almoravids.ContentManagement;

namespace Almoravids.Characters
{
    public class Archer : Enemy
    {
        private Arrow _arrow;
        private Texture2D _arrowTexture;
        private float _cooldown = 5f;
        private float _timer = 0f;
        private readonly float _speed; // get speed from constructor

        public Archer(Texture2D texture, Vector2 startPosition, Hero target, Texture2D questionTexture, ContentLoader contentLoader, string characterType = "archer", float speed = 0.00000001f)
            : base(texture, startPosition, target, questionTexture, characterType, speed)
        {
            _arrowTexture = contentLoader.LoadTexture2D("items/arrow");
            _speed = speed;
        }

        protected override void Attack(GameTime gameTime)
        {
            _timer -= (float)gameTime.ElapsedGameTime.TotalSeconds;
            if (_timer <= 0f)
            {
                Vector2 arrowDir = target.MovementComponent.Position - MovementComponent.Position;
                if (arrowDir.LengthSquared() > 0.01f)
                    arrowDir.Normalize();

                _arrow = new Arrow(_arrowTexture, MovementComponent.Position + new Vector2(16, 16), arrowDir);
                
                _timer = _cooldown;
            }

            _arrow?.Update(gameTime);

            if (_arrow != null && _arrow.IsActive && _arrow.BoundingBox.Intersects(target.CollisionComponent.BoundingBox))
            {
                // ADARGA CHECK
                if (target.Inventory.Contains("Adarga"))
                {
                    _arrow.Deactivate();
                }
                else
                {
                    Vector2 knockback = target.MovementComponent.Position - MovementComponent.Position;
                    target.HealthComponent.TakeDamage(1, knockback);
                    target.KnockbackComponent.ApplyKnockback(knockback);
                    _arrow.Deactivate();
                }
            }

            // facing correct way
            Vector2 direction = target.MovementComponent.Position - MovementComponent.Position;
            if (direction.LengthSquared() > 0.01f)
            {
                direction.Normalize();
                direction *= _speed; // speed for animation direction
                MovementComponent.SetDirection(direction);
            }
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);
            _arrow?.Draw(spriteBatch);
        }
    }
}