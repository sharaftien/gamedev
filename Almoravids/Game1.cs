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
        private Map map;
        private Hero hero;

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            _graphics.PreferredBackBufferWidth = 1248;
            _graphics.PreferredBackBufferHeight = 960;
            _graphics.ApplyChanges();
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            // Initialize Map
            map = new Map(Content, GraphicsDevice);  // Load the map with the Tiled map

            // Load Hero textures
            Texture2D walkTexture = Content.Load<Texture2D>("tashfin");
            Texture2D idleTexture = Content.Load<Texture2D>("tashfin_idle");
            Vector2 startPosition = new Vector2((800 / 2 - 50), (480 / 2 - 50));
            IInputReader inputReader = new KeyboardReader();
            hero = new Hero(idleTexture, walkTexture, startPosition, inputReader);
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            map.Update(gameTime);

            hero.Update(gameTime);  // Update hero (movement, animations, etc.)

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.BurlyWood);

            _spriteBatch.Begin();

            // Draw the map layers
            map.Draw(_spriteBatch);

            // Draw hero
            hero.Draw(_spriteBatch);

            _spriteBatch.End();

            base.Draw(gameTime);
        }
    }


}
