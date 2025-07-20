
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
            _enemySpawns = new List<(string Type, Vector2 Position)>
            {
                ("archer", new Vector2(100, 100)),
                ("archer", new Vector2(300, 400)),
                ("archer", new Vector2(600, 100)),
                ("archer", new Vector2(300, 10))
            };
            _itemSpawns = new List<(string Type, Vector2 Position)>
            {
                ("adarga", new Vector2(400, 200)),
                ("khuffayn", new Vector2(480, 200)),
                ("koumiya", new Vector2(560, 200)),
                ("litham", new Vector2(640, 200)),
                ("tasbih", new Vector2(720, 200))
            };
            _map = new Map(_contentLoader.LoadTiledMap(_mapName), _graphicsDevice);
        }
    }
}