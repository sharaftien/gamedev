
namespace Almoravids.Characters
{
    public class Swordsman : Enemy
    {
        private HealthComponent _healthComponent;
        private KnockbackComponent _knockbackComponent;
        private Texture2D _questionTexture;
        private float _rotation;
        private const float FullRotation = MathHelper.TwoPi;

        public Swordsman(Texture2D texture, Vector2 startPosition, Hero target, Texture2D questionTexture, string characterType = "swordsman", float speed = 80f)
            : base(texture, startPosition, target, characterType, speed)
        {
            _healthComponent = new HealthComponent(1);
            _knockbackComponent = new KnockbackComponent();
            CollisionComponent = new CollisionComponent(28f, 50f, 18f, 14f);
            _questionTexture = questionTexture;
            _rotation = 0f;
        }

        public override void Update(GameTime gameTime)
        {
            if (_healthComponent.IsAlive)
            {
                if (_knockbackComponent.KnockbackVelocity != Vector2.Zero)
                {
                    // knockback movement
                    MovementComponent.Position += _knockbackComponent.KnockbackVelocity * (float)gameTime.ElapsedGameTime.TotalSeconds;
                    MovementComponent.SetDirection(Vector2.Zero);
                    CollisionComponent.Update(MovementComponent.Position);
                }
                else
                {
                    base.Update(gameTime); // business as usual
                }

                // update question mark rotation when hero is invisible
                if (target.IsInvisible)
                {
                    _rotation += (float)gameTime.ElapsedGameTime.TotalSeconds * FullRotation / Almoravids.Items.Litham.InvisibilityDuration; // Rotate over Litham duration
                    if (_rotation >= FullRotation)
                    {
                        _rotation -= FullRotation; // reset
                    }
                }
            }
            else
            {
                MovementComponent.SetDirection(Vector2.Zero);
            }

            _healthComponent.Update(gameTime);
            _knockbackComponent.Update(gameTime);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            AnimationComponent.Draw(spriteBatch, MovementComponent.Position, Color.White);

            // draw question mark whenh ero is invisible
            if (target.IsInvisible)
            {
                Vector2 screenPosition = MovementComponent.Position + new Vector2(32f, -10f);
                Vector2 origin = new Vector2(_questionTexture.Width / 2f, _questionTexture.Height / 2f); // center of texture
                spriteBatch.Draw(_questionTexture, screenPosition, null, Color.White, _rotation, origin, 1f, SpriteEffects.None, 0f);
            }
        }

        public HealthComponent HealthComponent => _healthComponent;
        public KnockbackComponent KnockbackComponent => _knockbackComponent;
    }
}