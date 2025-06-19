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
        private Sahara_Swordsman swordman;

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

            // Load Hero texture
            Texture2D heroTexture = Content.Load<Texture2D>("tashfin");
            Vector2 startPosition = new Vector2(800 / 2 - 50, 480 / 2 - 50);
            InputManager inputManager = new InputManager();
            hero = new Hero(heroTexture, startPosition, inputManager, "hero", 100f);
            // Load swordman texture
            Texture2D swordmanTexture = Content.Load<Texture2D>("characters/lamtuni");
            swordman = new Sahara_Swordsman(swordmanTexture, new Vector2(100, 100), hero, "swordman", 80f);
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            map.Update(gameTime);
            hero.Update(gameTime);
            swordman.Update(gameTime); // Update enemy's position and behavior
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.BurlyWood);
            _spriteBatch.Begin();
            map.Draw(_spriteBatch);
            hero.Draw(_spriteBatch);
            swordman.Draw(_spriteBatch);
            _spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}