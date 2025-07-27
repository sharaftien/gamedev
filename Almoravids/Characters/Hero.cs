using Almoravids.Animation;
using Almoravids.Interfaces;
using Almoravids.Collision;
using Almoravids.Health;

namespace Almoravids.Characters
{
    public class Hero : Character, IControllable
    {
        public HealthComponent HealthComponent { get; private set; } // health component
        public List<string> Inventory { get; private set; } // store collected items
        public KnockbackComponent KnockbackComponent { get; private set; } // knockback component
        public bool IsInvisible { get; set; } // to check invisibility status
        public int BannerCount { get; private set; } // track collected banners

        public Hero(Texture2D texture, Vector2 startPosition, string characterType = "hero", float speed = 100f)
            : base(texture, startPosition, characterType, speed, accelerationRate: 250f, decelerationRate: 250f, maxSpeed: 135f)
        {
            HealthComponent = new HealthComponent(); // initialize HP (3 hearts)
            KnockbackComponent = new KnockbackComponent(); // initialize knockback
            Inventory = new List<string>(); // initializeer inventory
            // collision box for hero whitespace
            CollisionComponent = new CollisionComponent(28f, 50f, 18f, 14f); // 28x50 box with offset
            IsInvisible = false;
            BannerCount = 0; // initialize banner counter
        }

        public override void Update(GameTime gameTime)
        {
            if (HealthComponent.IsAlive)
            {
                // apply knockback velocity if active
                if (KnockbackComponent.KnockbackVelocity != Vector2.Zero)
                {
                    MovementComponent.OverrideVelocity(KnockbackComponent.KnockbackVelocity);
                    MovementComponent.SetDirection(Vector2.Zero);
                }
            }
            HealthComponent.Update(gameTime); // update invulnerability timer
            KnockbackComponent.Update(gameTime); // update knockback
            AnimationComponent.Update(gameTime, MovementComponent.Velocity, HealthComponent.IsAlive);
            MovementComponent.Update(gameTime);
            CollisionComponent.Update(MovementComponent.Position);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            // 50% opacity when invisible
            Color drawColor = IsInvisible ? new Color(Color.Gray, 0.5f) : Color.White;
            AnimationComponent.Draw(spriteBatch, MovementComponent.Position, drawColor);
        }

        // reset hero
        public override void Reset(Vector2 startPosition)
        {
            MovementComponent.Position = startPosition;
            HealthComponent.Heal(HealthComponent.MaxHealth);
            AnimationComponent.Reset(); // reset animation (AnimationComponent)
            Inventory.Clear();
            IsInvisible = false; // reset invisibility
            BannerCount = 0; // reset banner counter
        }

        // add item to inventory
        public void AddItem(string itemName)
        {
            Inventory.Add(itemName);
            Console.WriteLine($"Added {itemName} to inventory");
        }

        // add banner to counter
        public void AddBanner()
        {
            BannerCount++;
        }

        // IControllable implementation
        public void SetDirection(Vector2 direction)
        {
            MovementComponent.SetDirection(direction);
        }
    }
}