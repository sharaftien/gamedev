using Almoravids.Characters;
using Almoravids.Items;
using Almoravids.ContentManagement;

namespace Almoravids.Level
{
    public class LevelManager
    {
        private readonly ContentLoader _contentLoader; 
        private readonly GraphicsDevice _graphicsDevice;
        private Level _currentLevel;

        public LevelManager(ContentLoader contentLoader, GraphicsDevice graphicsDevice)
        {
            _contentLoader = contentLoader;
            _graphicsDevice = graphicsDevice;
        }

        public void LoadLevel(int level)
        {
            switch (level)
            {
                case 2:
                    _currentLevel = new Level_2(_contentLoader, _graphicsDevice);
                    break;

                case 3:
                    _currentLevel = new Level_3(_contentLoader, _graphicsDevice);
                    break;

                case 1:
                default:
                    _currentLevel = new Level_1(_contentLoader, _graphicsDevice);
                    break;
            }
            _currentLevel.Load();
        }

        public Map.Map Map => _currentLevel.Map;
        public Vector2 HeroSpawn => _currentLevel.HeroSpawn;
        public Vector2 TetherSpawn => _currentLevel.TetherSpawn;
        public List<EnemySpawn> EnemySpawns => _currentLevel.EnemySpawns; // enemy spawns
        public List<ItemSpawn> ItemSpawns => _currentLevel.ItemSpawns; // item spawns
    }
}