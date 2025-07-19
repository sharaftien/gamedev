
namespace Almoravids.Items
{
    public class Adarga : Item
    {
        public Adarga(Texture2D texture, Vector2 position)
            : base(texture, position)
        {
        }

        public override void OnPickup(Hero hero)
        {
            base.OnPickup(hero);
            hero.AddItem("Adarga");
        }

        public override void ApplyEffect(Hero hero, Enemy enemy)
        {
            Vector2 knockbackDirection = hero.MovementComponent.Position - enemy.MovementComponent.Position;
            enemy.KnockbackComponent.ApplyKnockback(-knockbackDirection); // push enemy back
            hero.Inventory.Remove("Adarga");
            Console.WriteLine("Adarga used to block attack and was consumed.");
        }
    }
}