
namespace Almoravids.GameState
{
    public class GameplayManager
    {
        private Map _map;
        private Hero _hero;
        private List<Enemy> _enemies;
        private List<Almoravids.Items.Item> _items; // explicit namespace for Item
        private Camera.Camera _camera;
        private Dictionary<Enemy, float> _collisionCooldowns; // check collision cooldowns per enemy
        private const float CollisionCooldownDuration = 1f; // avoid adarga knockback when walking towards enemy

        public GameplayManager(Map map, Hero hero, List<Enemy> enemies, List<Item> items, Camera.Camera camera)
        {
            _map = map;
            _hero = hero;
            _enemies = enemies;
            _items = items;
            _camera = camera;
            _collisionCooldowns = new Dictionary<Enemy, float>();
        }

        public void Update(GameTime gameTime)
        {
            _map.Update(gameTime);
            Vector2 heroProposedPosition = _hero.MovementComponent.Position;
            _hero.Update(gameTime);

            foreach (var enemy in _enemies)
            {
                enemy.Update(gameTime);

                // Enemy map collision
                if (enemy.CollisionComponent.CheckMapCollision(_map.CollisionLayer, out Vector2 enemyMapResolution))
                {
                    enemy.MovementComponent.Position += enemyMapResolution;
                    enemy.CollisionComponent.Update(enemy.MovementComponent.Position);
                }
            }

            if (_hero.CollisionComponent.CheckMapCollision(_map.CollisionLayer, out Vector2 heroMapResolution))
            {
                _hero.MovementComponent.Position = heroProposedPosition + heroMapResolution;
                _hero.CollisionComponent.Update(_hero.MovementComponent.Position);
            }

            // collision cooldowns
            var cooldownsToRemove = new List<Enemy>();
            foreach (var pair in _collisionCooldowns)
            {
                _collisionCooldowns[pair.Key] -= (float)gameTime.ElapsedGameTime.TotalSeconds;
                if (_collisionCooldowns[pair.Key] <= 0f)
                {
                    cooldownsToRemove.Add(pair.Key);
                }
            }
            foreach (var enemy in cooldownsToRemove)
            {
                _collisionCooldowns.Remove(enemy);
            }

            for (int i = _enemies.Count - 1; i >= 0; i--)
            {
                var enemy = _enemies[i];
                if (_collisionCooldowns.ContainsKey(enemy) || !enemy.HealthComponent.IsAlive)
                {
                    continue;
                }

                if (_hero.CollisionComponent.BoundingBox.Intersects(enemy.CollisionComponent.BoundingBox))
                {
                    Vector2 knockbackDirection = _hero.MovementComponent.Position - enemy.MovementComponent.Position;
                    if (_hero.Inventory.Contains("Koumiya"))
                    {
                        var koumiya = _items.FirstOrDefault(item => item is Koumiya) as Koumiya;
                        koumiya?.ApplyEffect(_hero, enemy); // damage enemy
                    }
                    else if (_hero.Inventory.Contains("Adarga"))
                    {
                        var adarga = _items.FirstOrDefault(item => item is Adarga) as Adarga;
                        adarga?.ApplyEffect(_hero, enemy); // Enemy is knocked back
                        // No hero damage or knockback
                    }
                    else
                    {
                        _hero.HealthComponent.TakeDamage(1, knockbackDirection);
                        _hero.KnockbackComponent.ApplyKnockback(knockbackDirection);
                    }
                    // add collision to cooldown
                    _collisionCooldowns[enemy] = CollisionCooldownDuration;
                }
            }

            if (_hero.HealthComponent.IsAlive)
            {
                // Check item collisions
                foreach (var item in _items)
                {
                    if (item is Banner banner)
                    {
                        banner.UpdateCollisionTimer(gameTime, _hero);
                    }
                    else if (item.IsActive && _hero.CollisionComponent.BoundingBox.Intersects(item.CollisionComponent.BoundingBox))
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