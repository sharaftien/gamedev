
namespace Almoravids.GameState
{
    public class GameplayInitializer
    {
        private readonly ContentLoader _contentLoader;
        private readonly GraphicsDevice _graphicsDevice;

        public GameplayInitializer(ContentLoader contentLoader, GraphicsDevice graphicsDevice)
        {
            _contentLoader = contentLoader;
            _graphicsDevice = graphicsDevice;
        }

        public (Map map, Hero hero, List<Enemy> enemies, List<Item> items, Camera.Camera camera, SpriteFont font) Initialize(int level)
        {
            var levelManager = new LevelManager(_contentLoader, _graphicsDevice); // initialize level manager
            levelManager.LoadLevel(level); // initialize level
            var map = levelManager.Map;
            var startPosition = levelManager.HeroSpawn; // hero spawn
            var enemyStartPositions = levelManager.EnemySpawns; // enemy spawn
            var itemSpawns = levelManager.ItemSpawns; // load items

            var font = _contentLoader.LoadSpriteFont("Fonts/Arial"); // load font for HP

            // initialize hero
            Texture2D heroTexture = _contentLoader.LoadTexture2D("tashfin");
            var hero = new Hero(heroTexture, startPosition, null, "hero", 100f);
            var inputManager = new InputManager(hero);
            hero.SetInputManager(inputManager);

            // initialize camera
            var camera = new Camera.Camera(startPosition);

            // initialize enemies
            Texture2D swordsmanTexture = _contentLoader.LoadTexture2D("characters/swordsman");
            Texture2D questionTexture = _contentLoader.LoadTexture2D("hud/question");

            var enemies = new List<Enemy>();
            foreach (var spawn in enemyStartPositions)
            {
                enemies.Add((Swordsman)EnemyFactory.Create("swordsman", swordsmanTexture, spawn, hero, questionTexture, 80f));
            }

            // initialize items
            var textureLoader = new ItemTextureLoader(_contentLoader);
            var textures = textureLoader.LoadItemTextures();
            var items = new List<Item>();
            foreach (var (type, position) in itemSpawns)
            {
                items.Add(ItemFactory.Create(type, textures[type], position));
            }

            return (map, hero, enemies, items, camera, font);
        }
    }
}