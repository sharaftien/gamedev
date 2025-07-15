
namespace Almoravids.Items
{
    public static class ItemFactory
    {
        public static Item Create(string type, Texture2D texture, Vector2 position)
        {
            switch (type)
            {
                case "adarga":
                    return new Adarga(texture, position);
                case "khuffayn":
                    return new Khuffayn(texture, position);
                case "koumiya":
                    return new Koumiya(texture, position);
                case "litham":
                    return new Litham(texture, position);
                case "tasbih":
                    return new Tasbih(texture, position);
                default:
                    throw new ArgumentException($"Unknown powerup type: {type}");
            }
        }
    }
}
