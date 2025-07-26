using Almoravids.Animation;
using Almoravids.Health;
using Almoravids.Collision;

namespace Almoravids.Characters
{
    public abstract class Enemy : Character
    {
        protected Hero target; // tashfin
        protected HealthComponent _healthComponent;
        protected KnockbackComponent _knockbackComponent;
        protected Texture2D _questionTexture;
        protected float _rotation;
        protected const float FullRotation = MathHelper.TwoPi;

        public Enemy(Texture2D texture, Vector2 startPosition, Hero target, Texture2D questionTexture, string characterType, float speed)
            : base(texture, startPosition, characterType, speed, 600f, 400f, speed, false)
        {
            this.target = target;
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
                    // start attack logic
                    if (!target.IsInvisible)
                    {
                        Attack(gameTime);
                    }
                    else
                    {
                        MovementComponent.SetDirection(Vector2.Zero); // else stop moving
                        // update question mark rotation when hero is invisible
                        _rotation += (float)gameTime.ElapsedGameTime.TotalSeconds * FullRotation / Almoravids.Items.Litham.InvisibilityDuration; // Rotate over Litham duration
                        if (_rotation >= FullRotation)
                        {
                            _rotation -= FullRotation; // reset
                        }
                    }
                    MovementComponent.Update(gameTime);
                    CollisionComponent.Update(MovementComponent.Position);
                }
            }

            _healthComponent.Update(gameTime);
            _knockbackComponent.Update(gameTime);
            AnimationComponent.Update(gameTime, MovementComponent.Velocity, _healthComponent.IsAlive);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            AnimationComponent.Draw(spriteBatch, MovementComponent.Position, Color.White);

            // draw question mark when hero is invisible
            if (target.IsInvisible)
            {
                Vector2 screenPosition = MovementComponent.Position + new Vector2(32f, -10f);
                Vector2 origin = new Vector2(_questionTexture.Width / 2f, _questionTexture.Height / 2f); // center of texture
                spriteBatch.Draw(_questionTexture, screenPosition, null, Color.White, _rotation, origin, 1f, SpriteEffects.None, 0f);
            }
        }

        public HealthComponent HealthComponent => _healthComponent;
        public KnockbackComponent KnockbackComponent => _knockbackComponent;

        protected abstract void Attack(GameTime gameTime); // attack logic
    }
}