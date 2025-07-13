
namespace Almoravids.GameState
{
    public class GameplayManager
    {
        private Map _map;
        private Hero _hero;
        private List<Swordsman> _swordsmen;
        private List<Almoravids.Items.Item> _items; // explicit namespace for Item
        private Camera.Camera _camera;

        public GameplayManager(Map map, Hero hero, List<Swordsman> swordsmen, List<Almoravids.Items.Item> items, Camera.Camera camera)
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

            List<Vector2> swordsmenProposedPositions = _swordsmen.Select(s => s.MovementComponent.Position).ToList();
            foreach (var swordsman in _swordsmen)
            {
                swordsman.Update(gameTime);
            }

            if (_hero.CollisionComponent.CheckMapCollision(_map.CollisionLayer, out Vector2 heroMapResolution))
            {
                _hero.MovementComponent.Position = heroProposedPosition + heroMapResolution;
                _hero.CollisionComponent.Update(_hero.MovementComponent.Position);
            }

            for (int i = 0; i < _swordsmen.Count; i++)
            {
                if (_swordsmen[i].CollisionComponent.CheckMapCollision(_map.CollisionLayer, out Vector2 swordsmanMapResolution))
                {
                    _swordsmen[i].MovementComponent.Position = swordsmenProposedPositions[i] + swordsmanMapResolution;
                    _swordsmen[i].CollisionComponent.Update(_swordsmen[i].MovementComponent.Position);
                }
            }

            if (_hero.HealthComponent.IsAlive)
            {
                foreach (var swordsman in _swordsmen)
                {
                    if (_hero.CollisionComponent.BoundingBox.Intersects(swordsman.CollisionComponent.BoundingBox))
                    {
                        Vector2 knockbackDirection = _hero.MovementComponent.Position - swordsman.MovementComponent.Position;
                        _hero.HealthComponent.TakeDamage(1, knockbackDirection);
                    }
                }

                // Check item collisions
                foreach (var item in _items)
                {
                    if (item.IsActive && _hero.CollisionComponent.BoundingBox.Intersects(item.CollisionComponent.BoundingBox))
                    {
                        Console.WriteLine($"Picking up item at {_hero.MovementComponent.Position}");
                        item.OnPickup(_hero);
                    }
                }
            }

            _camera.Update(_hero.MovementComponent.Position);
        }
    }
}