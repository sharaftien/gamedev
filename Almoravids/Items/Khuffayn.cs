
namespace Almoravids.Items
{
    public class Khuffayn : Item
    {
        public Khuffayn(Texture2D texture, Vector2 position)
            : base(texture, position)
        {
        }

        public override void OnPickup(Hero hero)
        {
            base.OnPickup(hero);
            hero.AddItem("Khuffayn");
        }
    }
}