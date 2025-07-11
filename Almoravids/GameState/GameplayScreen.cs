
namespace Almoravids.GameState
{
    public class GameplayScreen : IGameState
    {
        private Map map;
        private Hero hero;
        private List<Sahara_Swordsman> swordsmen; // multiple enemies -> list
        private Camera.Camera _camera;
        private SpriteFont _font; // for HP text
        private Vector2 _startPosition; // hero spawn
        private List<Vector2> _enemyStartPositions; // enemy spawn
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
            InitializeGameplay();
        }

        private void InitializeGameplay()
        {
            // initialize level
            _levelManager.LoadLevel(_level);
            map = _levelManager.Map;
            _startPosition = _levelManager.HeroSpawn;
            _enemyStartPositions = _levelManager.EnemySpawns;

            // initialize hero
            Texture2D heroTexture = _contentLoader.LoadTexture2D("tashfin");
            hero = new Hero(heroTexture, _startPosition, null, "hero", 100f);
            InputManager inputManager = new InputManager(hero);
            hero.SetInputManager(inputManager);

            // initialize camera
            _camera = new Camera.Camera(_startPosition);

            // initialize enemies
            Texture2D swordmanTexture = _contentLoader.LoadTexture2D("characters/lamtuni");
            swordsmen = new List<Sahara_Swordsman>();
            foreach (var pos in _enemyStartPositions)
            {
                swordsmen.Add(new Sahara_Swordsman(swordmanTexture, pos, hero, "swordman", 80f));
            }

            // initialize gameplay manager
            _gameplayManager = new GameplayManager(map, hero, swordsmen, _camera, _startPosition, _enemyStartPositions);
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
                foreach (var swordman in swordsmen)
                {
                    swordman.Draw(spriteBatch);
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