
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
            _enemySpawns = new List<Vector2>
            {
                new Vector2(100, 100),
                new Vector2(500, 500)
            }; // two enemies
            _map = new Map(_contentLoader.LoadTiledMap(_mapName), _graphicsDevice); // load map
        }
    }
}