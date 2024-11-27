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
    private TiledMap tiledMap; //holds Tiled map
    private Texture2D textureAtlas;
    private int tileSize;

    public Map(ContentManager content)
    {
        // Load the texture atlas for the map (assuming a single texture atlas for simplicity)
        textureAtlas = content.Load<Texture2D>("tiles/Terrain");
        tileSize = 48;  // Assuming all tiles are 48x48

        // Load the Tiled map (.tmx)
        tiledMap = content.Load<TiledMap>("sahara");  // Path to your .tmx file
    }
    

    public void Draw(SpriteBatch spriteBatch)
    {
        // Draw each layer in the Tiled map
        foreach (var layer in tiledMap.Layers)
        {
            if (layer is TiledMapTileLayer tileLayer) // Ensure it's a Tile Layer
            {
                DrawLayer(spriteBatch, tileLayer);
            }
        }
    }

    private void DrawLayer(SpriteBatch spriteBatch, TiledMapTileLayer tileLayer)
    {
        // Iterate through each tile in the layer
        foreach (var tile in tileLayer.Tiles)
        {
            // Check if the tile is blank (empty)
            if (tile.IsBlank) continue;

            // Use the TileIndex to find the correct tile in the texture atlas
            int tileId = tile.GlobalIdentifier - 1;  // Assuming the tile starts from 1

            // Calculate the source rectangle for the texture atlas
            int tilesPerRow = textureAtlas.Width / tileSize;
            int sourceX = (tileId % tilesPerRow) * tileSize;
            int sourceY = (tileId / tilesPerRow) * tileSize;

            Rectangle sourceRectangle = new Rectangle(sourceX, sourceY, tileSize, tileSize);
            Rectangle destinationRectangle = new Rectangle(tile.X * tileSize, tile.Y * tileSize, tileSize, tileSize);

            // Draw the tile
            spriteBatch.Draw(textureAtlas, destinationRectangle, sourceRectangle, Color.White);
        }
    }
}