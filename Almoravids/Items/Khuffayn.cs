using Almoravids.Characters;

namespace Almoravids.Items
{
    public class Khuffayn : Item
    {
        private const float SpeedIncrease = 1.5f; // 50% speed increase
        private const float Duration = 4.5f; // 4.5 seconds
        private float _timer;
        private Hero _hero; // store hero for speed reset
        private float _originalMaxSpeed; // store original speed for reset

        public Khuffayn(Texture2D texture, Vector2 position)
            : base(texture, position)
        {
            _timer = 0f;
            _hero = null;
            _originalMaxSpeed = 0f;
        }

        public override void OnPickup(Hero hero)
        {
            base.OnPickup(hero);
            hero.AddItem("Khuffayn");
            _hero = hero; // store hero for speed reset
            _originalMaxSpeed = hero.MovementComponent.MaxSpeed; // store original speed
            hero.MovementComponent.MaxSpeed *= SpeedIncrease; // apply speed increase
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
                    _hero.MovementComponent.MaxSpeed = _originalMaxSpeed; // reset to original speed
                    _hero.Inventory.Remove("Khuffayn"); // remove from inventory
                    Console.WriteLine("Khuffayn effect expired.");
                }
            }
        }

        public override void ApplyEffect(Hero hero, Enemy enemy)
        {
        }
    }
}