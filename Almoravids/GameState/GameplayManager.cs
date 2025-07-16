
namespace Almoravids.GameState
{
    public class GameplayManager
    {
        private Map _map;
        private Hero _hero;
        private List<Swordsman> _swordsmen;
        private List<Almoravids.Items.Item> _items; // explicit namespace for Item
        private Camera.Camera _camera;

        public GameplayManager(Map map, Hero hero, List<Swordsman> swordsmen, List<Item> items, Camera.Camera camera)
        {
            _map = map;
            _hero = hero;
            _swordsmen = swordsmen;
            _items = items;
            _camera = camera;
        }

        public void Update(GameTime gameTime)
        {
            _map.Update(gameTime);
            Vector2 heroProposedPosition = _hero.MovementComponent.Position;
            _hero.Update(gameTime);

            foreach (var swordsman in _swordsmen)
            {
                swordsman.Update(gameTime);
            }

            if (_hero.CollisionComponent.CheckMapCollision(_map.CollisionLayer, out Vector2 heroMapResolution))
            {
                _hero.MovementComponent.Position = heroProposedPosition + heroMapResolution;
                _hero.CollisionComponent.Update(_hero.MovementComponent.Position);
            }

            for (int i = _swordsmen.Count - 1; i >= 0; i--)
            {
                var swordsman = _swordsmen[i];
                if (_hero.CollisionComponent.BoundingBox.Intersects(swordsman.CollisionComponent.BoundingBox))
                {
                    Vector2 knockbackDirection = _hero.MovementComponent.Position - swordsman.MovementComponent.Position;
                    if (_hero.Inventory.Contains("Koumiya"))
                    {
                        var koumiya = _items.FirstOrDefault(item => item is Koumiya) as Koumiya;
                        koumiya?.ApplyEffect(_hero, swordsman);
                        if (!swordsman.HealthComponent.IsAlive)
                        {
                            _swordsmen.RemoveAt(i);
                            Console.WriteLine($"Swordsman killed at {swordsman.MovementComponent.Position}");
                        }
                    }
                    else if (_hero.Inventory.Contains("Adarga"))
                    {
                        var adarga = _items.FirstOrDefault(item => item is Adarga) as Adarga;
                        adarga?.ApplyEffect(_hero, swordsman);
                    }
                    else
                    {
                        _hero.HealthComponent.TakeDamage(1, knockbackDirection);
                        _hero.KnockbackComponent.ApplyKnockback(knockbackDirection);
                    }
                }
            }

            if (_hero.HealthComponent.IsAlive)
            {
                // Check item collisions
                foreach (var item in _items)
                {
                    if (item.IsActive && _hero.CollisionComponent.BoundingBox.Intersects(item.CollisionComponent.BoundingBox))
                    {
                        Console.WriteLine($"Picking up item at {_hero.MovementComponent.Position}");
                        item.OnPickup(_hero);
                    }
                    item.Update(gameTime); // update all items for timers (bvb Khuffayn)
                }
            }

            _camera.Update(_hero.MovementComponent.Position);
        }
    }
}