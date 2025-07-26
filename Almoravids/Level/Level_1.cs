
namespace Almoravids.Level
{
    public class Level_1 : Level
    {
        public Level_1(ContentLoader contentLoader, GraphicsDevice graphicsDevice)
            : base(contentLoader, graphicsDevice)
        {
        }

        public override void Load()
        {
            _mapName = "map/sahara"; // set map name
            _heroSpawn = new Vector2(300, 200);
            _enemySpawns = new List<EnemySpawn>
            {
                //new EnemySpawn { Type = "archer", Position = new Vector2(100, 100) },
                //new EnemySpawn { Type = "swordsman", Position = new Vector2(600, 100) },
                new EnemySpawn {
                    Type = "guard",
                    Position = new Vector2(400, 400),
                    PathPoints = new List<Vector2> {
                        new Vector2(400, 400),
                        new Vector2(600, 400)
                    },
                    Loop = false
                },
                // walk in square
                new EnemySpawn {
                    Type = "guard",
                    Position = new Vector2(200, 200),
                    PathPoints = new List<Vector2> {
                        new Vector2(200, 200),
                        new Vector2(300, 200),
                        new Vector2(300, 300),
                        new Vector2(200, 300)
                    },
                    WaitTimes = new List<float> { 0f, 2f, 0f, 2f },
                    Loop = true
                }
            };
            _itemSpawns = new List<ItemSpawn>
            {
                new ItemSpawn { Type = "adarga", Position = new Vector2(400, 200) },
                new ItemSpawn { Type = "khuffayn", Position = new Vector2(480, 200) },
                new ItemSpawn { Type = "koumiya", Position = new Vector2(560, 200) },
                new ItemSpawn { Type = "litham", Position = new Vector2(640, 200) },
                new ItemSpawn { Type = "tasbih", Position = new Vector2(720, 200) },
                new ItemSpawn { Type = "banner", Position = new Vector2(300, 300) },
                new ItemSpawn { Type = "banner", Position = new Vector2(500, 300) },
                new ItemSpawn { Type = "banner", Position = new Vector2(700, 300) },
                new ItemSpawn { Type = "bayaah", Position = new Vector2(800, 250) }
            };
            _map = new Map(_contentLoader.LoadTiledMap(_mapName), _graphicsDevice);
        }
    }
}