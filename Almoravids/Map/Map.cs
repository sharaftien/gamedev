using Microsoft.Xna.Framework.Content;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.IO; // Add this to fix the 'File' error
using MonoGame.Extended;
using MonoGame.Extended.Tiled;
using MonoGame.Extended.Tiled.Renderers;

public class Map
{
    private readonly TiledMap _tiledMap;
    private readonly TiledMapRenderer _tiledMapRenderer;
    private readonly TiledMapObjectLayer _collisionLayer;
    private const int TileSize = 48; // tiles 48x48.

    public Map(ContentManager content, GraphicsDevice graphicsDevice)
    {
        // Load the Tiled map
        _tiledMap = content.Load<TiledMap>("map/testing");
        _tiledMapRenderer = new TiledMapRenderer(graphicsDevice, _tiledMap);
        _collisionLayer = _tiledMap.GetLayer<TiledMapObjectLayer>("Oasis");
    }



    public void Update(GameTime gameTime)
    {
        // Update the map renderer if needed (e.g., for animated tiles)
        _tiledMapRenderer.Update(gameTime);
    }

    public void Draw(Matrix viewMatrix)
    {
        _tiledMapRenderer.Draw(viewMatrix);
    }

    public TiledMapObjectLayer CollisionLayer
    {
        get { return _collisionLayer; }
    }
}

