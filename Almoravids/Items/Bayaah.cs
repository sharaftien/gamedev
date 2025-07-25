
namespace Almoravids.Items
{
    public class Bayaah : Item
    {
        private const int RequiredBannerCount = 3;
        private readonly int _currentLevel;

        public Bayaah(Texture2D texture, Vector2 position, int currentLevel)
            : base(texture, position)
        {
            _currentLevel = currentLevel;
        }

        public override void OnPickup(Hero hero)
        {
            if (hero.BannerCount >= RequiredBannerCount)
            {
                base.OnPickup(hero);
                GameStateManager.Instance.SetState(new StageClearedScreen(_currentLevel));
                hero.AddItem("Bayaah");
            }
            else
            {
                Console.WriteLine($"Find all the banners first!");
            }
        }

        public override void Update(GameTime gameTime)
        {
        }
    }
}