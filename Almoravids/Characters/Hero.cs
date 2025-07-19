
namespace Almoravids.Characters
{
    public class Hero : Character
    {
        private InputManager _inputManager;
        public HealthComponent HealthComponent { get; private set; } // health component
        private bool _isDeadAnimationSet; // track if death animation is set
        public List<string> Inventory { get; private set; } // store collected items
        public KnockbackComponent KnockbackComponent { get; private set; } // knockback component
        public bool IsInvisible { get; set; } // to check invisibility status

        public Hero(Texture2D texture, Vector2 startPosition, InputManager inputManager, string characterType = "hero", float speed = 100f)
            : base(texture, startPosition, characterType, speed)
        {
            HealthComponent = new HealthComponent(); // initialize HP (3 hearts)
            KnockbackComponent = new KnockbackComponent(); // initialize knockback
            Inventory = new List<string>(); // initializeer inventory
            _inputManager = inputManager;
            if (_inputManager != null)
            {
                ConfigureInput();
            }
            // collision box for hero whitespace
            CollisionComponent = new CollisionComponent(28f, 50f, 18f, 14f); // 28x50 box with offset
            _isDeadAnimationSet = false;
            IsInvisible = false;
        }

        public void SetInputManager(InputManager inputManager)
        {
            _inputManager = inputManager;
            ConfigureInput();
        }

        private void ConfigureInput()
        {
            _inputManager.AddCommand(Keys.Left, new MoveCommand(_inputManager, new Vector2(-1, 0)));
            _inputManager.AddCommand(Keys.Right, new MoveCommand(_inputManager, new Vector2(1, 0)));
            _inputManager.AddCommand(Keys.Up, new MoveCommand(_inputManager, new Vector2(0, -1)));
            _inputManager.AddCommand(Keys.Down, new MoveCommand(_inputManager, new Vector2(0, 1)));
            // wasd
            _inputManager.AddCommand(Keys.Q, new MoveCommand(_inputManager, new Vector2(-1, 0)));
            _inputManager.AddCommand(Keys.D, new MoveCommand(_inputManager, new Vector2(1, 0)));
            _inputManager.AddCommand(Keys.Z, new MoveCommand(_inputManager, new Vector2(0, -1)));
            _inputManager.AddCommand(Keys.S, new MoveCommand(_inputManager, new Vector2(0, 1)));
        }

        public override void Update(GameTime gameTime)
        {
            if (HealthComponent.IsAlive)
            {
                _inputManager.Update(gameTime);
                // apply knockback velocity if active
                if (KnockbackComponent.KnockbackVelocity != Vector2.Zero)
                {
                    MovementComponent.OverrideVelocity(KnockbackComponent.KnockbackVelocity);
                    MovementComponent.SetDirection(Vector2.Zero);

                }
                _isDeadAnimationSet = false; // reset death bool
            }
            else
            {
                MovementComponent.Velocity = Vector2.Zero; // stop movement when dead
                if (!_isDeadAnimationSet)
                {
                    AnimationComponent.SetAnimation("death"); // play death animation
                    _isDeadAnimationSet = true;
                }
            }
            HealthComponent.Update(gameTime); // update invulnerability timer
            KnockbackComponent.Update(gameTime); // update knockback
            base.Update(gameTime);
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
            AnimationComponent.SetAnimation("idle_down"); // reset animation (idle)
            _isDeadAnimationSet = false;
            Inventory.Clear();
            IsInvisible = false; // reset invisibility
        }

        // add item to inventory
        public void AddItem(string itemName)
        {
            Inventory.Add(itemName);
            Console.WriteLine($"Added {itemName} to inventory");
        }
    }
}