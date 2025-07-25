
namespace Almoravids.Items
{
    public class Bayaah : Item
    {
        private const int RequiredBannerCount = 3;

        public Bayaah(Texture2D texture, Vector2 position)
            : base(texture, position)
        {
        }

        public override void OnPickup(Hero hero)
        {
            if (hero.BannerCount >= RequiredBannerCount)
            {
                base.OnPickup(hero);
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