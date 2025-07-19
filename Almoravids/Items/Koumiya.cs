
namespace Almoravids.Items
{
    public class Koumiya : Item
    {
        public Koumiya(Texture2D texture, Vector2 position)
            : base(texture, position)
        {
        }

        public override void OnPickup(Hero hero)
        {
            base.OnPickup(hero);
            hero.AddItem("Koumiya");
        }

        public override void ApplyEffect(Hero hero, Enemy enemy)
        {
            enemy.HealthComponent.TakeDamage(1, Vector2.Zero);
            hero.Inventory.Remove("Koumiya");
            Console.WriteLine("Koumiya used to deal damage and was consumed.");
        }
    }
}