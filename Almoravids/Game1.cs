using Almoravids.Characters;
using Almoravids.Input;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.IO;

namespace Almoravids
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        private Hero hero;

        private Dictionary<Vector2, int> bg;
        private Dictionary<Vector2, int> bnd;
        private Dictionary<Vector2, int> well;

        private Texture2D textureAtlas; 


        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            _graphics.PreferredBackBufferWidth = 1280; //1280 //1600 
            _graphics.PreferredBackBufferHeight = 720; //720 //900
            _graphics.ApplyChanges();

            Content.RootDirectory = "Content";
            IsMouseVisible = true;

            bg = LoadMap("../../../Data/maps/level_0/00_sahara_Background.csv");
            bnd = LoadMap("../../../Data/maps/level_0/00_sahara_Bounds.csv");
            well = LoadMap("../../../Data/maps/level_0/00_sahara_Well.csv");

        }

        private Dictionary<Vector2, int> LoadMap(string filepath){
            Dictionary<Vector2, int> result = new();

            StreamReader reader = new(filepath);

            int y = 0;
            string line;
            while ((line = reader.ReadLine()) != null) {

                string[] items = line.Split(',');

                for (int x = 0; x < items.Length; x++) {
                    if (int.TryParse(items[x], out int value))
                    {                    
                        if (value>-1)
                        {
                            result[new Vector2(x, y)] = value;
                        }
                    }
                }
                
                y++;
            }
            return result;
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            textureAtlas = Content.Load<Texture2D>("tiles/Terrain");

            // Load textures for Hero
            Texture2D walkTexture = Content.Load<Texture2D>("tashfin");
            Texture2D idleTexture = Content.Load<Texture2D>("tashfin_idle");
            
            // Set up initial position for Hero
            Vector2 startPosition = new Vector2((800/2), (480/2)); // Adjust starting position as needed (pixels ons screen)

            // Initialize input reader
            IInputReader inputReader = new KeyboardReader(); 

            // Initialize the Hero with the textures, start position, and input reader
            hero = new Hero(idleTexture, walkTexture, startPosition, inputReader); // Assign to field `hero`
            
            // TODO: use this.Content to load additional game content here if needed
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // TODO: Add your update logic here

            hero.Update(gameTime);

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.BurlyWood);

            // TODO: Add your drawing code here

            _spriteBatch.Begin();
            


            int display_tilesize = 64;
            int num_tiles_per_row = 8;
            int pixel_tilesize = 8;

            foreach (var item in bg)
            {
                Rectangle drect = new(
                    (int)item.Key.X * display_tilesize,
                    (int)item.Key.Y * display_tilesize,
                    display_tilesize,
                    display_tilesize
                    );

                int x = item.Value % num_tiles_per_row;
                int y = item.Value / num_tiles_per_row;
                Rectangle src = new(
                    x * pixel_tilesize,
                    y * pixel_tilesize,
                    pixel_tilesize,
                    pixel_tilesize
                    );

                _spriteBatch.Draw(textureAtlas, drect, src, Color.White);
            }

            hero.Draw(_spriteBatch);

            _spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
