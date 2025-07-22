
namespace Almoravids.Level
{
    public abstract class Level
    {
        protected readonly ContentLoader _contentLoader; // store content loader
        protected readonly GraphicsDevice _graphicsDevice; // store graphics device
        protected Map _map; // store map
        protected Vector2 _heroSpawn; // store hero spawn position
        protected List<EnemySpawnInfo> _enemySpawns; // store enemy spawn positions
        protected List<(string Type, Vector2 Position)> _itemSpawns; // store item spawn positions
        protected string _mapName; // store map name

        protected Level(ContentLoader contentLoader, GraphicsDevice graphicsDevice)
        {
            _contentLoader = contentLoader; // initialize content loader
            _graphicsDevice = graphicsDevice; // initialize graphics device
            _enemySpawns = new List<EnemySpawnInfo>();
            _itemSpawns = new List<(string Type, Vector2 Position)>();
        }

        public abstract void Load(); // load level-specific data and map

        public Map Map => _map;
        public Vector2 HeroSpawn => _heroSpawn;
        public List<EnemySpawnInfo> EnemySpawns => _enemySpawns; // return EnemySpawnInfo list
        public List<(string Type, Vector2 Position)> ItemSpawns => _itemSpawns;
    }
}