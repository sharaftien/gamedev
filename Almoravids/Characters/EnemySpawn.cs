
namespace Almoravids.Characters
{
    public class EnemySpawn
    {
        public string Type { get; set; }
        public Vector2 Position { get; set; }
        public List<Vector2> PathPoints { get; set; } = null; // guard walking path
        public bool? Loop { get; set; } = null; // guard loop or pingpong
        public List<float> WaitTimes { get; set; } = null; // guard seconds per point
    }
} 