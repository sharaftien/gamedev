
namespace Almoravids.Characters
{
    public static class EnemyFactory
    {
        private static readonly Dictionary<string, Func<Texture2D, Vector2, Hero, Texture2D, ContentLoader, float, Enemy>> _enemyCreators = new()
        {
            { "swordsman", (texture, position, target, questionTexture, contentLoader, speed) => new Swordsman(texture, position, target, questionTexture, "swordsman", speed) },
            { "archer", (texture, position, target, questionTexture, contentLoader, speed) => new Archer(texture, position, target, questionTexture, contentLoader, "archer", speed) },
            { "guard", (texture, position, target, questionTexture, contentLoader, speed) => new Guard(texture, position, target, questionTexture, "guard", speed) }
};

        public static Enemy Create(string type, Texture2D texture, Vector2 position, Hero target, Texture2D questionTexture, ContentLoader contentLoader, float speed = 80f)
        {
            if (_enemyCreators.TryGetValue(type, out var creator))
            {
                return creator(texture, position, target, questionTexture, contentLoader, speed);
            }
            throw new ArgumentException($"Unknown enemy type: {type}");
        }
    }
}