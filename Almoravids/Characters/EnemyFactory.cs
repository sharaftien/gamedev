using Almoravids.ContentManagement;

namespace Almoravids.Characters
{
    public static class EnemyFactory
    {
        private static readonly Dictionary<string, Func<Texture2D, Vector2, Hero, Texture2D, ContentLoader, float, List<Vector2>, List<float>, Enemy>> _enemyCreators = new()
        {
            { "swordsman", (texture, position, target, questionTexture, contentLoader, speed, guardPath, waitTimes) => new Swordsman(texture, position, target, questionTexture, "swordsman", speed) },
            { "archer", (texture, position, target, questionTexture, contentLoader, speed, guardPath, waitTimes) => new Archer(texture, position, target, questionTexture, contentLoader, "archer", speed) },
            { "guard", (texture, position, target, questionTexture, contentLoader, speed, guardPath, waitTimes) => new Guard(texture, position, target, questionTexture, "guard", speed, guardPath, waitTimes) }
};

        public static Enemy Create(string type, Texture2D texture, Vector2 position, Hero target, Texture2D questionTexture, ContentLoader contentLoader, float speed = 30f, List<Vector2> guardPath = null, List<float> waitTimes = null)
        {
            float enemySpeed = type switch
            {
                "archer" => 0.00000000000000000001f, // stand still
                "swordsman" => 20f,   // normal
                "guard" => 85f,       // slow
                _ => throw new NotImplementedException(),
            };

            if (_enemyCreators.TryGetValue(type, out var creator))
            {
                return creator(texture, position, target, questionTexture, contentLoader, enemySpeed, guardPath, waitTimes);
            }
            throw new ArgumentException($"Unknown enemy type: {type}");
        }
    }
}