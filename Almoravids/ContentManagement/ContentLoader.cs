
namespace Almoravids.ContentManagement
{
    public class ContentLoader
    {
        private readonly ContentManager _contentManager;

        public ContentLoader(ContentManager contentManager)
        {
            _contentManager = contentManager;
        }

        public SpriteFont LoadSpriteFont(string path)
        {
            return _contentManager.Load<SpriteFont>(path);
        }

        public TiledMap LoadTiledMap(string path)
        {
            return _contentManager.Load<TiledMap>(path);
        }

        public Texture2D LoadTexture2D(string path)
        {
            return _contentManager.Load<Texture2D>(path);
        }
    }
}