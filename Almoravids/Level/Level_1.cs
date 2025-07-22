
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
            _mapName = "map/testing"; // set map name
            _heroSpawn = new Vector2(300, 200);
            _enemySpawns = new List<EnemySpawn>
            {
                new EnemySpawn { Type = "archer", Position = new Vector2(100, 100) },
                new EnemySpawn { Type = "swordsman", Position = new Vector2(600, 100) },
                new EnemySpawn { Type = "guard", Position = new Vector2(400, 400) }
            };
            _itemSpawns = new List<ItemSpawn>
            {
                new ItemSpawn { Type = "adarga", Position = new Vector2(400, 200) },
                new ItemSpawn { Type = "khuffayn", Position = new Vector2(480, 200) },
                new ItemSpawn { Type = "koumiya", Position = new Vector2(560, 200) },
                new ItemSpawn { Type = "litham", Position = new Vector2(640, 200) },
                new ItemSpawn { Type = "tasbih", Position = new Vector2(720, 200) }
            };
            _map = new Map(_contentLoader.LoadTiledMap(_mapName), _graphicsDevice);
        }
    }
}