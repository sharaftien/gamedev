
namespace Almoravids.GameState
{
    public class GameplayManager
    {
        private readonly Map _map;
        private readonly Hero _hero;
        private readonly List<Sahara_Swordsman> _swordsmen;
        private readonly Camera.Camera _camera;
        private readonly Vector2 _startPosition;
        private readonly List<Vector2> _enemyStartPositions;

        public GameplayManager(Map map, Hero hero, List<Sahara_Swordsman> swordsmen, Camera.Camera camera, Vector2 startPosition, List<Vector2> enemyStartPositions)
        {
            _map = map;
            _hero = hero;
            _swordsmen = swordsmen;
            _camera = camera;
            _startPosition = startPosition;
            _enemyStartPositions = enemyStartPositions;
        }

        public void Update(GameTime gameTime)
        {
            // update gameplay
            _map.Update(gameTime);

            // update hero
            Vector2 heroProposedPosition = _hero.MovementComponent.Position;
            _hero.Update(gameTime);

            // update enemies
            List<Vector2> swordsmenProposedPositions = _swordsmen.Select(s => s.MovementComponent.Position).ToList();
            foreach (var swordman in _swordsmen)
            {
                swordman.Update(gameTime);
            }

            // check map collisions
            if (_hero.CollisionComponent.CheckMapCollision(_map.CollisionLayer, out Vector2 heroMapResolution))
            {
                _hero.MovementComponent.Position = heroProposedPosition + heroMapResolution;
                _hero.CollisionComponent.Update(_hero.MovementComponent.Position);
            }

            // check map collisions for enemies
            for (int i = 0; i < _swordsmen.Count; i++)
            {
                if (_swordsmen[i].CollisionComponent.CheckMapCollision(_map.CollisionLayer, out Vector2 swordmanMapResolution))
                {
                    _swordsmen[i].MovementComponent.Position = swordsmenProposedPositions[i] + swordmanMapResolution;
                    _swordsmen[i].CollisionComponent.Update(_swordsmen[i].MovementComponent.Position);
                }
            }

            // check enemy collisions with hero
            if (_hero.HealthComponent.IsAlive)
            {
                foreach (var swordman in _swordsmen)
                {
                    if (_hero.CollisionComponent.BoundingBox.Intersects(swordman.CollisionComponent.BoundingBox))
                    {
                        Vector2 knockbackDirection = _hero.MovementComponent.Position - swordman.MovementComponent.Position;
                        _hero.HealthComponent.TakeDamage(1, knockbackDirection);
                    }
                }
            }

            // Update camera
            _camera.Update(_hero.MovementComponent.Position);
        }
    }
}