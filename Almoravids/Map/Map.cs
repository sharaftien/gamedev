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

public class Map
{
    private TiledMap tiledMap; // Holds the Tiled map
    private int tileSize;

    public Map(ContentManager content)
    {
        // Load the Tiled map (.tmx)
        tiledMap = content.Load<TiledMap>("map/winlu_sahara");  // Adjust path as per your project structure
        tileSize = 48;  // Assuming tiles are 48x48
    }

    public void Draw(SpriteBatch spriteBatch)
    {
        // Draw each layer in the Tiled map
        foreach (var layer in tiledMap.Layers)
        {
            if (layer is TiledMapTileLayer tileLayer) // Ensure it's a tile layer
            {
                DrawLayer(spriteBatch, tileLayer);
            }
        }
    }

    private void DrawLayer(SpriteBatch spriteBatch, TiledMapTileLayer tileLayer)
    {


        foreach (var tile in tileLayer.Tiles)
        {
            if (tile.IsBlank) continue;

            // Determine which tileset the tile belongs to
            var tileset = tiledMap.GetTilesetByTileGlobalIdentifier(tile.GlobalIdentifier);

            // Get the texture and calculate source rectangle
            var tilesetTexture = tileset.Texture;
            int localTileId = tile.GlobalIdentifier - tileset.Tiles.First().LocalTileIdentifier;
            int tilesPerRow = tilesetTexture.Width / tileset.TileWidth;
            int sourceX = (localTileId % tilesPerRow) * tileset.TileWidth;
            int sourceY = (localTileId / tilesPerRow) * tileset.TileHeight;

            Rectangle sourceRectangle = new Rectangle(sourceX, sourceY, tileset.TileWidth, tileset.TileHeight);
            Rectangle destinationRectangle = new Rectangle(tile.X * tileSize, tile.Y * tileSize, tileSize, tileSize);

            spriteBatch.Draw(tilesetTexture, destinationRectangle, sourceRectangle, Color.White);
        }
    }
}
