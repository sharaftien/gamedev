using Almoravids.ContentManagement;

namespace Almoravids.Level
{
    public class Level_3 : Level
    {
        public Level_3(ContentLoader contentLoader, GraphicsDevice graphicsDevice)
            : base(contentLoader, graphicsDevice)
        {
        }

        public override void Load()
        {
            _mapName = "map/gibraltar"; // set map name
            _heroSpawn = new Vector2(800 / 2 - 50, 480 / 2 - 50); // hero spawn
            _map = new Map.Map(_contentLoader.LoadTiledMap(_mapName), _graphicsDevice);
        }
    }
}