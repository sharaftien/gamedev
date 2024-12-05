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
    private const int TileSize = 48; // tiles 48x48.

    public Map(ContentManager content, GraphicsDevice graphicsDevice)
    {
        // Load the Tiled map
        _tiledMap = content.Load<TiledMap>("map/sahara");
        _tiledMapRenderer = new TiledMapRenderer(graphicsDevice, _tiledMap);
    }



    public void Update(GameTime gameTime)
    {
        // Update the map renderer if needed (e.g., for animated tiles)
        _tiledMapRenderer.Update(gameTime);
    }

    public void Draw(SpriteBatch spriteBatch)
    {
        // Use the TiledMapRenderer to draw all layers
        _tiledMapRenderer.Draw();
    }
}

