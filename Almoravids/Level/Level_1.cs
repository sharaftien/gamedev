
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
            _itemSpawns = new List<Vector2>{new Vector2(400, 200), new Vector2(480, 200) }; // koumiya spawn
            _map = new Map(_contentLoader.LoadTiledMap(_mapName), _graphicsDevice); // load map
        }
    }
}
