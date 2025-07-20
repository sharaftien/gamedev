
namespace Almoravids.Characters
{
    public class Archer : Enemy
    {
        private Arrow _arrow;
        private Texture2D _arrowTexture;
        private float _cooldown = 4f;
        private float _timer = 0f;

        public Archer(Texture2D texture, Vector2 startPosition, Hero target, Texture2D questionTexture, ContentLoader contentLoader, string characterType = "archer", float speed = 10f)
            : base(texture, startPosition, target, questionTexture, characterType, speed)
        {
            _arrowTexture = contentLoader.LoadTexture2D("items/arrow");
        }

        protected override void Attack(GameTime gameTime)
        {
            _timer -= (float)gameTime.ElapsedGameTime.TotalSeconds;
            if (_timer <= 0f)
            {
                _arrow = new Arrow(_arrowTexture, MovementComponent.Position + new Vector2(16, 16), Vector2.UnitY);
                _timer = _cooldown;
            }

            _arrow?.Update(gameTime);

            if (_arrow != null && _arrow.IsActive && _arrow.BoundingBox.Intersects(target.CollisionComponent.BoundingBox))
            {
                Vector2 knockback = target.MovementComponent.Position - MovementComponent.Position;
                target.HealthComponent.TakeDamage(1, knockback);
                target.KnockbackComponent.ApplyKnockback(knockback);
                _arrow.Deactivate();
            }

            // facing correct way
            MovementComponent.SetDirection(target.MovementComponent.Position - MovementComponent.Position);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);
            _arrow?.Draw(spriteBatch);
        }
    }
}