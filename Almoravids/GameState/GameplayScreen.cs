
namespace Almoravids.GameState
{
    public class GameplayScreen : IGameState
    {
        private Map map;
        private Hero hero;
        private List<Swordsman> swordsmen; // multiple enemies -> list
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

            // initialize hero
            Texture2D heroTexture = _contentLoader.LoadTexture2D("tashfin");
            hero = new Hero(heroTexture, startPosition, null, "hero", 100f);
            InputManager inputManager = new InputManager(hero);
            hero.SetInputManager(inputManager);

            // initialize camera
            _camera = new Camera.Camera(startPosition);

            // initialize enemies
            Texture2D swordsmanTexture = _contentLoader.LoadTexture2D("characters/lamtuni");
            swordsmen = new List<Swordsman>();
            foreach (var pos in enemyStartPositions)
            {
                swordsmen.Add(new Swordsman(swordsmanTexture, pos, hero, "swordsman", 80f));
            }

            // initialize gameplay manager
            _gameplayManager = new GameplayManager(map, hero, swordsmen, _camera);
        }

        public void Update(GameTime gameTime)
        {
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
                spriteBatch.End();
                spriteBatch.Begin(samplerState: SamplerState.PointClamp);
                // HP
                spriteBatch.DrawString(_font, $"Alive: {hero.HealthComponent.IsAlive}", new Vector2(10, 35), Color.White);
                spriteBatch.DrawString(_font, $"HP: {hero.HealthComponent.CurrentHealth}/{hero.HealthComponent.MaxHealth}", new Vector2(10, 10), Color.White);
            }
            spriteBatch.End();
        }
    }
}