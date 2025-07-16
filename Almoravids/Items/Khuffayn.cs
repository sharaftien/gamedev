
namespace Almoravids.Items
{
    public class Khuffayn : Item
    {
        private const float SpeedIncrease = 1.8f; // 50% speed increase
        private const float Duration = 7f; // 7 seconds
        private float _timer;
        private Hero _hero; // store hero for speed reset
        private float _originalSpeed; // store original speed for reset

        public Khuffayn(Texture2D texture, Vector2 position)
            : base(texture, position)
        {
            _timer = 0f;
            _hero = null;
            _originalSpeed = 0f;
        }

        public override void OnPickup(Hero hero)
        {
            base.OnPickup(hero);
            hero.AddItem("Khuffayn");
            _hero = hero; // store hero for speed reset
            _originalSpeed = hero.MovementComponent.Speed; // store original speed
            hero.MovementComponent.Speed *= SpeedIncrease; // apply speed increase
            _timer = Duration;
            Console.WriteLine($"Khuffayn picked up, speed increased by {SpeedIncrease} for {Duration} seconds.");
        }

        public override void Update(GameTime gameTime)
        {
            if (_timer > 0f)
            {
                _timer -= (float)gameTime.ElapsedGameTime.TotalSeconds;
                if (_timer <= 0f && _hero != null)
                {
                    _hero.MovementComponent.Speed = _originalSpeed; // reset to original speed
                    _hero.Inventory.Remove("Khuffayn"); // remove from inventory
                    Console.WriteLine("Khuffayn effect expired.");
                }
            }
        }

        public override void ApplyEffect(Hero hero, Swordsman enemy)
        {
        }
    }
}