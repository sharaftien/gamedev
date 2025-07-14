
namespace Almoravids.Characters
{
    public static class EnemyFactory
    {
        public static Enemy Create(string type, Texture2D texture, Vector2 position, Hero target, float speed = 80f)
        {
            switch (type)
            {
                case "swordsman":
                    return new Swordsman(texture, position, target, "swordsman", speed);
                case "archer":
                   // return new Archer(texture, position, target, "swordsman", speed); //soon
                case "guard":
                  //  return new Guard (texture, position, target, "swordsman", speed); //soon
                default:
                    throw new ArgumentException($"Unknown enemy type: {type}");
            }
        }
    }
}