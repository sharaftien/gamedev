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

            // initialize hero without inputmanger
            hero = new Hero(heroTexture, startPosition, null, "hero", 100f);
            // create inputmanager with hero
            InputManager inputManager = new InputManager(hero);
            hero.SetInputManager(inputManager);

            // Load swordman texture
            Texture2D swordmanTexture = Content.Load<Texture2D>("characters/lamtuni");
            swordman = new Sahara_Swordsman(swordmanTexture, new Vector2(100, 100), hero, "swordman", 80f);
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            map.Update(gameTime);

            // proposed positions
            Vector2 heroProposedPosition = hero.MovementComponent.Position;
            hero.Update(gameTime);
            Vector2 swordmanProposedPosition = swordman.MovementComponent.Position;
            swordman.Update(gameTime);

            // check map collisions
            if (hero.CollisionComponent.CheckMapCollision(map.CollisionLayer, out Vector2 heroMapResolution))
            {
                hero.MovementComponent.Position = heroProposedPosition + heroMapResolution;
                hero.CollisionComponent.Update(hero.MovementComponent.Position);
            }
            if (swordman.CollisionComponent.CheckMapCollision(map.CollisionLayer, out Vector2 swordmanMapResolution))
            {
                swordman.MovementComponent.Position = swordmanProposedPosition + swordmanMapResolution;
                swordman.CollisionComponent.Update(swordman.MovementComponent.Position);
            }
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