using MonoGame.Extended.Tiled;
using MonoGame.Extended.Tiled.Renderers;

namespace Almoravids.Map
{
    public class Map
    {
        private readonly TiledMap _tiledMap;
        private readonly TiledMapRenderer _tiledMapRenderer;
        private readonly TiledMapObjectLayer _collisionLayer;
        private const int TileSize = 48; // tiles 48x48.

        public Map(TiledMap tiledMap, GraphicsDevice graphicsDevice)
        {
            _tiledMap = tiledMap;
            _tiledMapRenderer = new TiledMapRenderer(graphicsDevice, _tiledMap);
            _collisionLayer = _tiledMap.GetLayer<TiledMapObjectLayer>("Oasis");
        }

        public void Update(GameTime gameTime)
        {
            // Update the map renderer if needed (e.g., for animated tiles)
            _tiledMapRenderer.Update(gameTime);
        }

        public void DrawWithoutTrees(Matrix viewMatrix)
        {
            // draw everything except trees
            for (int i = 0; i < _tiledMap.TileLayers.Count; i++)
            {
                var layer = _tiledMap.TileLayers[i];
                if (layer.Name != "Trees")
                {
                    _tiledMapRenderer.Draw(layer, viewMatrix);
                }
            }
        }

        public void DrawTrees(Matrix viewMatrix)
        {
            // only draw trees
            TiledMapTileLayer treesLayer = _tiledMap.GetLayer<TiledMapTileLayer>("Trees");
            if (treesLayer != null)
            {
                _tiledMapRenderer.Draw(treesLayer, viewMatrix);
            }
        }

        public TiledMapObjectLayer CollisionLayer
        {
            get { return _collisionLayer; }
        }
    }
}
