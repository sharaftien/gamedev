using Almoravids.Characters;
using Almoravids.Input;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.IO;
using Almoravids.Camera;

namespace Almoravids
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        private Map map;
        private Hero hero;
        private Sahara_Swordsman swordman;
        private Camera.Camera _camera;
        private SpriteFont _arialFont; // added for HP text
        private Vector2 _startPosition; // hero spawn
        private Vector2 _enemyStartPosition; // enemy spawn

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            _graphics.PreferredBackBufferWidth = 960; // 1248
            _graphics.PreferredBackBufferHeight = 720; // 960
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

            // initialize map
            map = new Map(Content, GraphicsDevice);

            // load hero texture
            Texture2D heroTexture = Content.Load<Texture2D>("tashfin");
            _startPosition = new Vector2(800 / 2 - 50, 480 / 2 - 50);
            hero = new Hero(heroTexture, _startPosition, null, "hero", 100f);
            InputManager inputManager = new InputManager(hero);
            hero.SetInputManager(inputManager);

            // initialize camera
            _camera = new Camera.Camera(_startPosition);

            // load swordman texture
            Texture2D swordmanTexture = Content.Load<Texture2D>("characters/lamtuni");
            _enemyStartPosition = new Vector2(100, 100);
            swordman = new Sahara_Swordsman(swordmanTexture, _enemyStartPosition, hero, "swordman", 80f);

            // load font
            _arialFont = Content.Load<SpriteFont>("Fonts/Arial");
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

            // check enemy collision
            if (hero.HealthComponent.IsAlive && hero.CollisionComponent.BoundingBox.Intersects(swordman.CollisionComponent.BoundingBox))
            {
                Vector2 knockbackDirection = hero.MovementComponent.Position - swordman.MovementComponent.Position;
                hero.HealthComponent.TakeDamage(1, knockbackDirection);
            }

            // check for restart
            if (!hero.HealthComponent.IsAlive && Keyboard.GetState().IsKeyDown(Keys.R))
            {
                hero.Reset(_startPosition);
                swordman.Reset(_enemyStartPosition);
                _camera.Update(_startPosition);
            }

            // update camera
            _camera.Update(hero.MovementComponent.Position);

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.BurlyWood);

            // use camera transform
            var transformMatrix = _camera.GetTransformMatrix();

            _spriteBatch.Begin(transformMatrix: transformMatrix, samplerState: SamplerState.PointClamp);
            map.Draw(transformMatrix); 
            hero.Draw(_spriteBatch);
            swordman.Draw(_spriteBatch);
            _spriteBatch.End();

            // HP
            _spriteBatch.Begin(samplerState: SamplerState.PointClamp);
            _spriteBatch.DrawString(_arialFont, $"Alive: {hero.HealthComponent.IsAlive}", new Vector2(10, 35), Color.White);
            _spriteBatch.DrawString(_arialFont, $"HP: {hero.HealthComponent.CurrentHealth}/{hero.HealthComponent.MaxHealth}", new Vector2(10, 10), Color.White);

            // restart
            if (!hero.HealthComponent.IsAlive)
            {
                Vector2 youDiedPosition = new Vector2((830) / 2, (720) / 2);
                Vector2 pressRPosition = new Vector2((730) / 2, (400));
                _spriteBatch.DrawString(_arialFont, "You died!", youDiedPosition, Color.Red);
                _spriteBatch.DrawString(_arialFont, "Press \"R\" to restart.", pressRPosition, Color.White);
            }
            _spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}