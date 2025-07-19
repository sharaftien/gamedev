
namespace Almoravids.Items
{
    public static class ItemFactory
    {
        private static readonly Dictionary<string, Func<Texture2D, Vector2, Item>> _itemCreators = new()
        {
            { "adarga", (texture, position) => new Adarga(texture, position) },
            { "khuffayn", (texture, position) => new Khuffayn(texture, position) },
            { "koumiya", (texture, position) => new Koumiya(texture, position) },
            { "litham", (texture, position) => new Litham(texture, position) },
            { "tasbih", (texture, position) => new Tasbih(texture, position) }
        };

        public static Item Create(string type, Texture2D texture, Vector2 position)
        {
            if (!_itemCreators.TryGetValue(type, out var creator))
            {
                throw new ArgumentException($"Unknown powerup type: {type}");
            }
            return creator(texture, position);
        }

    }
}