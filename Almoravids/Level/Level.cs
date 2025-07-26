using Almoravids.Characters;
using Almoravids.Items;
using Almoravids.ContentManagement;

namespace Almoravids.Level
{
    public abstract class Level
    {
        protected readonly ContentLoader _contentLoader; // store content loader
        protected readonly GraphicsDevice _graphicsDevice; // store graphics device
        protected Map.Map _map; // store map
        protected Vector2 _heroSpawn; // store hero spawn position
        protected List<EnemySpawn> _enemySpawns; // store enemy spawn positions
        protected List<ItemSpawn> _itemSpawns; // store item spawn positions
        protected string _mapName; // store map name

        protected Level(ContentLoader contentLoader, GraphicsDevice graphicsDevice)
        {
            _contentLoader = contentLoader; // initialize content loader
            _graphicsDevice = graphicsDevice; // initialize graphics device
            _enemySpawns = new List<EnemySpawn>();
            _itemSpawns = new List<ItemSpawn>();
        }

        public abstract void Load(); // load level-specific data and map

        public Almoravids.Map.Map Map => _map;
        public Vector2 HeroSpawn => _heroSpawn;
        public List<EnemySpawn> EnemySpawns => _enemySpawns;
        public List<ItemSpawn> ItemSpawns => _itemSpawns;
    }
}