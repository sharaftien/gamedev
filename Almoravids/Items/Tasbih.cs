
namespace Almoravids.Items
{
    public class Tasbih : Item
    {
        private const float ActivationDuration = 5f; 
        private Hero _hero;
        private bool _isActivating; // check if spatiebalk is being held
        private float _activationTimer; // check for how long

        public Tasbih(Texture2D texture, Vector2 position)
            : base(texture, position)
        {
            _activationTimer = 0f;
            _hero = null;
            _isActivating = false;
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
                    }
                    _activationTimer -= (float)gameTime.ElapsedGameTime.TotalSeconds;
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

        public override void ApplyEffect(Hero hero, Swordsman enemy)
        {
        }
    }
}