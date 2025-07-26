using Almoravids.Characters;
using Almoravids.Items;
using Almoravids.ContentManagement;

namespace Almoravids.Level
{
    public class Level_2 : Level
    {
        public Level_2(ContentLoader contentLoader, GraphicsDevice graphicsDevice)
            : base(contentLoader, graphicsDevice)
        {
        }

        public override void Load()
        {
            _mapName = "map/marrakech"; // set map name
            _heroSpawn = new Vector2(800 / 2 - 50, 480 / 2 - 50); // hero spawn
            _enemySpawns = new List<EnemySpawn>
            {
                new EnemySpawn { Type = "archer", Position = new Vector2(100, 100) },
                new EnemySpawn { Type = "swordsman", Position = new Vector2(500, 500) },
                new EnemySpawn { Type = "guard", Position = new Vector2(200, 200) }
            };
            _itemSpawns = new List<ItemSpawn>
            {
                new ItemSpawn { Type = "khuffayn", Position = new Vector2(250, 300) },
                new ItemSpawn { Type = "litham", Position = new Vector2(250, 400) }
            };
            _map = new Map.Map(_contentLoader.LoadTiledMap(_mapName), _graphicsDevice);
        }
    }
}