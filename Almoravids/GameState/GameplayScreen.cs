
namespace Almoravids.GameState
{
    public class GameplayScreen : IGameState
    {
        private Map map;
        private Hero hero;
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
            (map, hero, enemies, items, _camera, _font, _inputSystem) = initializer.Initialize(_level); // setup gameplay

            // initialize gameplay manager
            _gameplayManager = new GameplayManager(map, hero, enemies, items, _camera);
        }

        public void Update(GameTime gameTime)
        {
            // Handle input for restart and level menu
            var keyboardState = Keyboard.GetState();
            if (keyboardState.IsKeyDown(Keys.R))
            {
                GameStateManager.Instance.SetState(new GameplayScreen(_level)); // restart
            }
            if (keyboardState.IsKeyDown(Keys.Enter))
            {
                GameStateManager.Instance.SetState(new LevelScreen()); // return to level menu
            }

            if (!_gameOver && !hero.HealthComponent.IsAlive)
            {
                GameStateManager.Instance.SetState(new GameOverScreen(_level));
                _gameOver = true;
            }
            else
            {
                _inputSystem.Update(gameTime); // update character inputs
                _gameplayManager.Update(gameTime);
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Begin();
            if (map != null)
            {
                var transformMatrix = _camera.GetTransformMatrix();
                spriteBatch.End();

                // draw map background without trees
                spriteBatch.Begin(transformMatrix: transformMatrix, samplerState: SamplerState.PointClamp);
                map.DrawWithoutTrees(transformMatrix);
                spriteBatch.End();

                // draw items
                spriteBatch.Begin(transformMatrix: transformMatrix, samplerState: SamplerState.PointClamp);
                foreach (var item in items)
                {
                    item.Draw(spriteBatch);
                }
                spriteBatch.End();

                // draw enemies
                spriteBatch.Begin(transformMatrix: transformMatrix, samplerState: SamplerState.PointClamp);
                foreach (var enemy in enemies)
                {
                    enemy.Draw(spriteBatch);
                }
                spriteBatch.End();

                // draw hero
                spriteBatch.Begin(transformMatrix: transformMatrix, samplerState: SamplerState.PointClamp);
                hero.Draw(spriteBatch);
                spriteBatch.End();

                // draw trees
                spriteBatch.Begin(transformMatrix: transformMatrix, samplerState: SamplerState.PointClamp);
                map.DrawTrees(transformMatrix);
                spriteBatch.End();

                // draw UI
                spriteBatch.Begin(samplerState: SamplerState.PointClamp);
                // HP
                string statusText = hero.HealthComponent.IsInvulnerable ? "You're hit!" : $"{hero.HealthComponent.CurrentHealth}HP";
                spriteBatch.DrawString(_font, $"Health: {statusText}", new Vector2(10, 5), Color.White);
                // Inventory
                spriteBatch.DrawString(_font, $"Inventory: {string.Join(", ", hero.Inventory)}", new Vector2(10, 35), Color.White);
                // Banner count
                spriteBatch.DrawString(_font, $"Banners: {hero.BannerCount}", new Vector2(10, 65), Color.White);
            }
            spriteBatch.End();
        }
    }
}