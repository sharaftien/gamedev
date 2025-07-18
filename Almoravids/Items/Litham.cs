
namespace Almoravids.Items
{
    public class Litham : Item
    {
        public const float InvisibilityDuration = 7f; // 7 seconds invisibility
        private float _timer;
        private Hero _hero;

        public Litham(Texture2D texture, Vector2 position)
            : base(texture, position)
        {
            _timer = 0f;
            _hero = null;
        }

        public override void OnPickup(Hero hero)
        {
            base.OnPickup(hero);
            hero.AddItem("Litham");
            _hero = hero;
            _hero.IsInvisible = true; // make hero invisible
            _timer = InvisibilityDuration;
            Console.WriteLine($"Litham picked up! Hero is invisible for {InvisibilityDuration} seconds.");
        }

        public override void Update(GameTime gameTime)
        {
            if (_timer > 0f)
            {
                _timer -= (float)gameTime.ElapsedGameTime.TotalSeconds;
                if (_timer <= 0f && _hero != null)
                {
                    _hero.IsInvisible = false; // reset invisibility
                    _hero.Inventory.Remove("Litham");
                    Console.WriteLine("Litham effect expired.");
                }
            }
        }
    }
}