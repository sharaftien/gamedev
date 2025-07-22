
namespace Almoravids.Characters
{
    public class EnemySpawnInfo
    {
        public string Type { get; set; }
        public Vector2 Position { get; set; }
        public List<Vector2> PathPoints { get; set; } = null; // guard walking path
        public bool? Loop { get; set; } = null; // guard loop or pingpong
    }
} 