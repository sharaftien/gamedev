using Microsoft.Xna.Framework.Content;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.IO; // Add this to fix the 'File' error

public class Map
{
    private readonly Dictionary<Vector2, int> background;
    private readonly Dictionary<Vector2, int> collisions;
    private readonly Dictionary<Vector2, int> towerCosmetics;
    private readonly Dictionary<Vector2, int> towerBorders;
    private readonly Dictionary<Vector2, int> towerBase;
    private readonly Dictionary<Vector2, int> treesAndNature;
    private readonly Dictionary<Vector2, int> smallItems;
    private readonly Dictionary<Vector2, int> wellLayer;
    private readonly Dictionary<Vector2, int> rugsLayer;
    private readonly Dictionary<Vector2, int> duneBackground;

    private readonly Texture2D textureAtlas;
    private readonly int tileSize;

    public Map(ContentManager content)
    {
        // Load the texture atlas for all the layers (assuming a single texture atlas for simplicity)
        textureAtlas = content.Load<Texture2D>("tiles/Terrain");
        tileSize = 48;  // Assuming all tiles are 48x48

        // Load each map layer
        background = LoadMap("Data/maps/sahara/sahara_Background_Transparent.csv");
        collisions = LoadMap("Data/maps/sahara/sahara_collisions.csv");
        towerCosmetics = LoadMap("Data/maps/sahara/sahara_Tower_Tower_Cosmetics.csv");
        towerBorders = LoadMap("Data/maps/sahara/sahara_Tower_Tower_border_en_windows.csv");
        towerBase = LoadMap("Data/maps/sahara/sahara_Tower_Tower.csv");
        treesAndNature = LoadMap("Data/maps/sahara/sahara_Trees_and_nature.csv");
        smallItems = LoadMap("Data/maps/sahara/sahara_small_items.csv");
        wellLayer = LoadMap("Data/maps/sahara/sahara_Well.csv");
        rugsLayer = LoadMap("Data/maps/sahara/sahara_rugs.csv");
        duneBackground = LoadMap("Data/maps/sahara/sahara_sand_background.csv");
    }

    private Dictionary<Vector2, int> LoadMap(string filepath)
    {
        Dictionary<Vector2, int> result = new Dictionary<Vector2, int>();

        // Construct the full path using the base directory
        string fullPath = Path.Combine(AppContext.BaseDirectory, filepath);
        Console.WriteLine($"Looking for file at: {fullPath}"); // Debugging output

        // Read all lines from the file
        string[] lines = File.ReadAllLines(fullPath);

        int y = 0;
        foreach (string line in lines)
        {
            string[] items = line.Split(',');

            for (int x = 0; x < items.Length; x++)
            {
                if (int.TryParse(items[x], out int value) && value > -1)
                {
                    result[new Vector2(x, y)] = value;
                }
            }
            y++;
        }

        return result;
    }

    public void Draw(SpriteBatch spriteBatch)
    {
        // Draw background layer first (sand where the character walks)
        DrawLayer(spriteBatch, background);

        // Draw tower layers
        DrawLayer(spriteBatch, towerCosmetics);
        DrawLayer(spriteBatch, towerBorders);
        DrawLayer(spriteBatch, towerBase);

        // Draw trees, nature, and small items on top
        DrawLayer(spriteBatch, treesAndNature);
        DrawLayer(spriteBatch, smallItems);

        // Draw rugs and well for interaction
        DrawLayer(spriteBatch, rugsLayer);
        DrawLayer(spriteBatch, wellLayer);

        // Draw dune background and any other decorative layer
        DrawLayer(spriteBatch, duneBackground);
    }

    private void DrawLayer(SpriteBatch spriteBatch, Dictionary<Vector2, int> layer)
    {
        foreach (var item in layer)
        {
            int x = item.Value % 8; // Get the tile's x position in the texture atlas
            int y = item.Value / 8; // Get the tile's y position in the texture atlas

            Rectangle sourceRectangle = new Rectangle(x * tileSize, y * tileSize, tileSize, tileSize);
            Rectangle destinationRectangle = new Rectangle((int)item.Key.X * tileSize, (int)item.Key.Y * tileSize, tileSize, tileSize);

            spriteBatch.Draw(textureAtlas, destinationRectangle, sourceRectangle, Color.White);
        }
    }
}