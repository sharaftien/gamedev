using Almoravids.ContentManagement;

namespace Almoravids.Items
{
    public static class ItemFactory
    {
        private static readonly Dictionary<string, Func<Texture2D, Vector2, ContentLoader, Item>> _itemCreators = new()
        {
            { "adarga", (texture, position, contentLoader) => new Adarga(texture, position) },
            { "khuffayn", (texture, position, contentLoader) => new Khuffayn(texture, position) },
            { "koumiya", (texture, position, contentLoader) => new Koumiya(texture, position) },
            { "litham", (texture, position, contentLoader) => new Litham(texture, position) },
            { "tasbih", (texture, position, contentLoader) => new Tasbih(texture, position) },
            { "banner", (texture, position, contentLoader) => new Banner(texture, position, contentLoader) },
            { "bayaah", (texture, position, contentLoader) => new Bayaah(texture, position, 1) } // default level gets overidden
        };

        public static Item Create(string type, Texture2D texture, Vector2 position, ContentLoader contentLoader = null)
        {
            if (!_itemCreators.TryGetValue(type, out var creator))
            {
                throw new ArgumentException($"Unknown powerup type: {type}");
            }
            return creator(texture, position, contentLoader);
        }

    }
}