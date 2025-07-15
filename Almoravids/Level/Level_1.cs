
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
            _heroSpawn = new Vector2(800 / 2 - 50, 480 / 2 - 50); // hero spawn
            _enemySpawns = new List<Vector2> { new Vector2(100, 100) }; // one enemy
            _itemSpawns = new List<(string Type, Vector2 Position)>
            {
                ("adarga", new Vector2(400, 200)), // Koumiya op (400, 200)
                ("khuffayn", new Vector2(480, 200)),  // Adarga op (480, 200)
                ("koumiya", new Vector2(560, 200)), // Khuffayn op (560, 200)
                ("litham", new Vector2(640, 200)),  // Litham op (640, 200)
                ("tasbih", new Vector2(720, 200))   // Tasbih op (720, 200)
            };
            _map = new Map(_contentLoader.LoadTiledMap(_mapName), _graphicsDevice);
        }
    }
}