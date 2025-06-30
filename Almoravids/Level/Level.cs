
namespace Almoravids.Level
{
    public abstract class Level
    {
        protected readonly ContentLoader _contentLoader; // store content loader
        protected readonly GraphicsDevice _graphicsDevice; // store graphics device
        protected Map _map; // store map
        protected Vector2 _heroSpawn; // store hero spawn position
        protected List<Vector2> _enemySpawns; // store enemy spawn positions
        protected string _mapName; // store map name

        protected Level(ContentLoader contentLoader, GraphicsDevice graphicsDevice)
        {
            _contentLoader = contentLoader; // initialize content loader
            _graphicsDevice = graphicsDevice; // initialize graphics device
        }

        public abstract void Load(); // load level-specific data and map

        public Map Map
        {
            get { return _map; }
        }

        public Vector2 HeroSpawn
        {
            get { return _heroSpawn; }
        }

        public List<Vector2> EnemySpawns
        {
            get { return _enemySpawns; }
        }


    }
}