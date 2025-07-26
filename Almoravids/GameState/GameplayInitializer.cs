using Almoravids.Characters;
using Almoravids.Items;
using Almoravids.Input;
using Almoravids.Level;
using Almoravids.ContentManagement;

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

        public (Almoravids.Map.Map map, Hero hero, List<Enemy> enemies, List<Item> items, Camera.Camera camera, SpriteFont font, InputSystem inputSystem) Initialize(int level)
        {
            var levelManager = new LevelManager(_contentLoader, _graphicsDevice); // initialize level manager
            levelManager.LoadLevel(level); // initialize level
            var map = levelManager.Map;
            var startPosition = levelManager.HeroSpawn; // hero spawn
            var enemySpawns = levelManager.EnemySpawns; // enemy spawn
            var itemSpawns = levelManager.ItemSpawns; // load items

            var font = _contentLoader.LoadSpriteFont("Fonts/Arial"); // load font for HP

            // initialize hero
            var heroTexture = _contentLoader.LoadTexture2D("tashfin");
            var hero = new Hero(heroTexture, startPosition, "hero", 100f);
            var inputManager = new InputManager(hero);

            // initialize input system
            var inputSystem = new InputSystem();
            inputSystem.AddInputManager(inputManager, hero); // register input manager for hero

            // initialize camera
            var camera = new Camera.Camera(startPosition);

            // initialize enemies
            var swordsmanTexture = _contentLoader.LoadTexture2D("characters/swordsman");
            var archerTexture = _contentLoader.LoadTexture2D("characters/archer");
            var guardTexture = _contentLoader.LoadTexture2D("characters/guard");
            var questionTexture = _contentLoader.LoadTexture2D("hud/question");

            var enemies = new List<Enemy>();
            foreach (var spawn in enemySpawns)
            {
                var type = spawn.Type;
                var position = spawn.Position;
                var texture = type switch
                {
                    "swordsman" => swordsmanTexture,
                    "archer" => archerTexture,
                    "guard" => guardTexture,
                };
                List<Vector2> guardPath = null;
                List<float> waitTimes = null;
                if (type == "guard")
                {
                    guardPath = spawn.PathPoints ?? new List<Vector2> { position, position + new Vector2(0, 200) };
                    waitTimes = spawn.WaitTimes;
                }
                enemies.Add(EnemyFactory.Create(type, texture, position, hero, questionTexture, _contentLoader, 80f, guardPath, waitTimes));
            }

            // initialize items
            var textureLoader = new ItemTextureLoader(_contentLoader);
            var textures = textureLoader.LoadItemTextures();
            var items = new List<Item>();
            foreach (var spawn in itemSpawns)
            {
                var type = spawn.Type;
                var position = spawn.Position;
                if (type == "bayaah")
                {
                    items.Add(new Bayaah(textures[type], position, level)); // pass level to bayaah class
                }
                else
                {
                    items.Add(ItemFactory.Create(type, textures[type], position, _contentLoader));
                }
            }

            return (map, hero, enemies, items, camera, font, inputSystem);
        }
    }
}