using Almoravids.Characters;
using Almoravids.Items;
using Almoravids.Input;
using Almoravids.ContentManagement;
using Almoravids.Interfaces;

namespace Almoravids.GameState
{
    public class GameplayScreen : IGameState
    {
        private Map.Map map;
        private Hero hero;
        private Tether tether;
        private List<Enemy> enemies; // multiple enemies -> list
        private List<Item> items; // explicit namespace for Item
        private Camera.Camera _camera;
        private SpriteFont _font; // for HP text
        private ContentManager _content; // store content
        private GraphicsDevice _graphicsDevice; // store graphics device
        private int _level; // store selected level
        private GameplayManager _gameplayManager; // manage game logic
        private bool _gameOver; // track if game over was triggered
        private InputSystem _inputSystem; // manage character inputs
        private List<Particles.Particle> _particles = new(); // bloedpartikels
        private IGameState _nextState = null;
        public IGameState GetNextState()
        {
            return _nextState;
        }

        public GameplayScreen(int level = 1)
        {
            _level = level; // set level
            _gameOver = false;
        }

        public void Initialize(ContentManager content, GraphicsDevice graphicsDevice)
        {
            _content = content; // store content
            _graphicsDevice = graphicsDevice; // store graphics device

            var contentLoader = new ContentLoader(content); // initialize content loader
            var initializer = new GameplayInitializer(contentLoader, graphicsDevice); // initialize helper
            (map, hero, tether, enemies, items, _camera, _font, _inputSystem) = initializer.Initialize(_level); // setup gameplay

            // initialize gameplay manager
            _gameplayManager = new GameplayManager(map, hero, enemies, items, _camera);
        }

        public void Update(GameTime gameTime)
        {
            // Handle input for restart and level menu
            var keyboardState = Keyboard.GetState();
            if (keyboardState.IsKeyDown(Keys.R))
            {
                _nextState = new GameplayScreen(_level); ; // restart
            }
            if (keyboardState.IsKeyDown(Keys.Enter))
            {
                _nextState = new LevelScreen(); ; ; // return to level menu
            }

            if (!_gameOver && !hero.HealthComponent.IsAlive)
            {
                _nextState = new GameOverScreen(_level); ; ;
                _gameOver = true;
            }
            else
            {
                _inputSystem.Update(gameTime); // update character inputs
                tether.Update(gameTime);

                if (hero.HealthComponent.IsInvulnerable)
                {
                    for (int i = 0; i < 2; i++)
                    {
                        Vector2 dir = new Vector2(
                            (float)(Random.Shared.NextDouble() * 2 - 1),
                            (float)(Random.Shared.NextDouble() * 2 - 1)
                        );
                        if (dir != Vector2.Zero)
                            dir.Normalize();
                        _particles.Add(new Particles.BloodParticle(hero.MovementComponent.Position + new Vector2(24, 24), dir));
                    }
                }

                _gameplayManager.Update(gameTime);

                foreach (var p in _particles)
                    p.Update(gameTime);
                _particles.RemoveAll(p => p.IsDead);
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Begin();
            if (map != null)
            {
                var snappedMatrix = _camera.GetSnappedTransformMatrix();
                var smoothMatrix = _camera.GetSmoothTransformMatrix();
                spriteBatch.End();

                // draw map background without trees
                spriteBatch.Begin(sortMode: SpriteSortMode.Deferred, transformMatrix: snappedMatrix, samplerState: SamplerState.PointClamp);
                map.DrawWithoutTrees(snappedMatrix);
                spriteBatch.End();

                // draw items
                spriteBatch.Begin(sortMode: SpriteSortMode.Deferred, transformMatrix: smoothMatrix, samplerState: SamplerState.PointClamp);
                foreach (var item in items)
                {
                    item.Draw(spriteBatch);
                }
                spriteBatch.End();

                // draw enemies
                spriteBatch.Begin(sortMode: SpriteSortMode.Deferred, transformMatrix: smoothMatrix, samplerState: SamplerState.PointClamp);
                foreach (var enemy in enemies)
                {
                    enemy.Draw(spriteBatch);
                }
                spriteBatch.End();

                // draw hero + blood
                spriteBatch.Begin(sortMode: SpriteSortMode.Deferred, transformMatrix: smoothMatrix, samplerState: SamplerState.PointClamp);
                foreach (var p in _particles)
                    p.Draw(spriteBatch);
                hero.Draw(spriteBatch);                
                spriteBatch.End();

                // draw tether
                spriteBatch.Begin(sortMode: SpriteSortMode.Deferred, transformMatrix: smoothMatrix, samplerState: SamplerState.PointClamp);
                tether.Draw(spriteBatch);
                spriteBatch.End();

                // draw trees (snapped)
                spriteBatch.Begin(sortMode: SpriteSortMode.Deferred, transformMatrix: snappedMatrix, samplerState: SamplerState.PointClamp);
                map.DrawTrees(snappedMatrix);
                spriteBatch.End();

                // draw UI
                spriteBatch.Begin(sortMode: SpriteSortMode.Deferred, samplerState: SamplerState.PointClamp);
                // HP
                string statusText = hero.HealthComponent.IsInvulnerable ? "You're hit!" : $"{hero.HealthComponent.CurrentHealth}HP";
                spriteBatch.DrawString(_font, $"Health: {statusText}", new Vector2(10, 5), Color.White);
                // Inventory
                spriteBatch.DrawString(_font, $"Inventory: {string.Join(", ", hero.Inventory)}", new Vector2(10, 35), Color.White);
                // Banner count
                spriteBatch.DrawString(_font, $"Banners: {hero.BannerCount}", new Vector2(10, 65), Color.White);
                spriteBatch.End();
            }
            else
            {
                spriteBatch.End();
            }
        }
    }
}