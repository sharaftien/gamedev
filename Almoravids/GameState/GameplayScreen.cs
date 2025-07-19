
namespace Almoravids.GameState
{
    public class GameplayScreen : IGameState
    {
        private Map map;
        private Hero hero;
        private List<Swordsman> swordsmen; // multiple enemies -> list
        private List<Item> items; // explicit namespace for Item
        private Camera.Camera _camera;
        private SpriteFont _font; // for HP text
        private ContentManager _content; // store content
        private GraphicsDevice _graphicsDevice; // store graphics device
        private int _level; // store selected level
        private ContentLoader _contentLoader; // store content loader
        private LevelManager _levelManager; // store level manager
        private GameplayManager _gameplayManager; // manage game logic
        private bool _gameOver; // track if game over was triggered

        public GameplayScreen(int level = 1)
        {
            _level = level; // set level
            _gameOver = false;
        }

        public void Initialize(ContentManager content, GraphicsDevice graphicsDevice)
        {
            _content = content; // store content
            _graphicsDevice = graphicsDevice; // store graphics device
            _contentLoader = new ContentLoader(content); // initialize content loader
            _levelManager = new LevelManager(_contentLoader, _graphicsDevice); // initialize level manager
            _font = _contentLoader.LoadSpriteFont("Fonts/Arial"); // load font for HP

            // initialize level
            _levelManager.LoadLevel(_level);
            map = _levelManager.Map;
            var startPosition = _levelManager.HeroSpawn; // hero spawn
            var enemyStartPositions = _levelManager.EnemySpawns; // enemy spawn
            var itemSpawns = _levelManager.ItemSpawns; // load items
            items = new List<Almoravids.Items.Item>();

            // initialize hero
            Texture2D heroTexture = _contentLoader.LoadTexture2D("tashfin");
            hero = new Hero(heroTexture, startPosition, null, "hero", 100f);
            InputManager inputManager = new InputManager(hero);
            hero.SetInputManager(inputManager);

            // initialize camera
            _camera = new Camera.Camera(startPosition);

            // initialize enemies
            Texture2D swordsmanTexture = _contentLoader.LoadTexture2D("characters/swordsman");
            Texture2D questionTexture = _contentLoader.LoadTexture2D("hud/question");
            swordsmen = new List<Swordsman>();
            foreach (var spawn in enemyStartPositions)
            {
                swordsmen.Add((Swordsman)EnemyFactory.Create("swordsman", swordsmanTexture, spawn, hero, questionTexture, 80f));
            }

            // initialize items
            var textureLoader = new ItemTextureLoader(_contentLoader);
            var textures = textureLoader.LoadItemTextures();
            foreach (var (type, position) in itemSpawns)
            {
                items.Add(ItemFactory.Create(type, textures[type], position));
            }

            // initialize gameplay manager
            _gameplayManager = new GameplayManager(map, hero, swordsmen, items, _camera);
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
                GameStateManager.Instance.SetState(new GameOverScreen());
                _gameOver = true;
            }
            else
            {
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
                spriteBatch.Begin(transformMatrix: transformMatrix, samplerState: SamplerState.PointClamp);
                map.Draw(transformMatrix);
                hero.Draw(spriteBatch);
                foreach (var swordsman in swordsmen)
                {
                    swordsman.Draw(spriteBatch);
                }
                foreach (var item in items)
                {
                    item.Draw(spriteBatch);
                }
                spriteBatch.End();
                spriteBatch.Begin(samplerState: SamplerState.PointClamp);
                // HP
                string statusText = hero.HealthComponent.IsInvulnerable ? "You're hit!" : $"{hero.HealthComponent.CurrentHealth}HP";
                spriteBatch.DrawString(_font, $"Health: {statusText}", new Vector2(10, 5), Color.White);
                // Inventory
                spriteBatch.DrawString(_font, $"Inventory: {string.Join(", ", hero.Inventory)}", new Vector2(10, 35), Color.White);
            }
            spriteBatch.End();
        }
    }
}