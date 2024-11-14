using Almoravids.Characters;
using Almoravids.Input;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;

namespace Almoravids
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        private Hero hero;

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            _graphics.PreferredBackBufferWidth = 1280; //1280 //1600 
            _graphics.PreferredBackBufferHeight = 720; //720 //900
            _graphics.ApplyChanges();

            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            // Load textures for Hero
            Texture2D walkTexture = Content.Load<Texture2D>("tashfin");
            Texture2D idleTexture = Content.Load<Texture2D>("tashfin_idle");
            
            // Set up initial position for Hero
            Vector2 startPosition = new Vector2((1280/2)-64, (720/2)-64); // Adjust starting position as needed (pixels ons screen)

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
            hero.Draw(_spriteBatch);
            _spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
