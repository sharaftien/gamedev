
namespace Almoravids.Items
{
    public class Tasbih : Item
    {
        private Hero _hero;

        private const float ActivationDuration = 5f; // 5 seconds to activate
        private bool _isActivating; // check if spatiebalk is being held
        private float _activationTimer; // check for how long          

        private float _rotation;
        private const float FullRotation = MathHelper.TwoPi;

        public Tasbih(Texture2D texture, Vector2 position)
            : base(texture, position)
        {
            _activationTimer = 0f;
            _hero = null;
            _isActivating = false;
            _rotation = 0f;
        }

        public override void OnPickup(Hero hero)
        {
            base.OnPickup(hero);
            hero.AddItem("Tasbih");
            _hero = hero; // store hero for activation
        }

        public override void Update(GameTime gameTime)
        {
            if (_hero != null && _hero.HealthComponent.IsAlive && _hero.Inventory.Contains("Tasbih"))
            {
                if (Keyboard.GetState().IsKeyDown(Keys.Space))
                {
                    if (!_isActivating)
                    {
                        _isActivating = true;
                        _activationTimer = ActivationDuration;
                        _rotation = 0f; // reset rotation
                    }
                    _activationTimer -= (float)gameTime.ElapsedGameTime.TotalSeconds;
                    _rotation = FullRotation * (1f - _activationTimer / ActivationDuration); // rotation lasts 5 seconds
                    Console.WriteLine($"Praying: {_activationTimer:F2} seconds remaining.");
                    if (_activationTimer <= 0f)
                    {
                        _hero.HealthComponent.Heal(2);
                        _hero.Inventory.Remove("Tasbih");
                        _isActivating = false;
                        Console.WriteLine("You are filled with determination. HP restored");
                    }
                }
                else if (_isActivating)
                {
                    _isActivating = false;
                    Console.WriteLine("Stopped mid prayer.");
                }
            }
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            if (_isActive)
            {
                base.Draw(spriteBatch); // draw on map when active
            }
            if (_isActivating && _hero != null)
            {
                // draw tasbih above hero
                Vector2 heroScreenPosition = _hero.MovementComponent.Position;
                Vector2 screenPosition = heroScreenPosition + new Vector2(32f, -10f);
                Vector2 origin = new Vector2(_texture.Width / 2f, _texture.Height / 2f); // center of texture
                spriteBatch.Draw(_texture, screenPosition, null, Color.White, _rotation, origin, 1f, SpriteEffects.None, 0f);
            }
        }

        public override void ApplyEffect(Hero hero, Swordsman enemy)
        {
        }
    }
}