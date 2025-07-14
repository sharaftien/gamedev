
namespace Almoravids.Items
{
    public static class ItemFactory
    {
        public static Item Create(string type, Texture2D texture, Vector2 position)
        {
            switch (type)
            {
                case "adarga":
                    return new Koumiya(texture, position);
                case "khuffayn":
                    return new Koumiya(texture, position);
                case "koumiya":
                    return new Koumiya(texture, position);
                case "litham":
                    return new Koumiya(texture, position);
                case "tasbih":
                    return new Koumiya(texture, position);
                default:
                    throw new ArgumentException($"Unknown powerup type: {type}");
            }
        }
    }
}
