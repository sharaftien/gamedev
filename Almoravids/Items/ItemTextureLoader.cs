
namespace Almoravids.Items
{
    public class ItemTextureLoader
    {
        private readonly ContentLoader _contentLoader;

        public ItemTextureLoader(ContentLoader contentLoader)
        {
            _contentLoader = contentLoader;
        }

        public Dictionary<string, Texture2D> LoadItemTextures()
        {
            return new Dictionary<string, Texture2D>
            {
                { "adarga", _contentLoader.LoadTexture2D("Items/adarga") },
                { "khuffayn", _contentLoader.LoadTexture2D("Items/khuffayn") },
                { "koumiya", _contentLoader.LoadTexture2D("Items/koumiya") },
                { "litham", _contentLoader.LoadTexture2D("Items/litham") },
                { "tasbih", _contentLoader.LoadTexture2D("Items/tasbih") },
                { "banner", _contentLoader.LoadTexture2D("items/banner") }
            };
        }
    }
}