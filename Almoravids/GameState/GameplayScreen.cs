namespace Almoravids.GameState
{
    public class GameplayScreen : IGameState
    {
        private Map map;
        private Hero hero;
        private Sahara_Swordsman swordman;
        private Camera.Camera _camera;
        private SpriteFont _font; // for HP text
        private Vector2 _startPosition; // hero spawn
        private Vector2 _enemyStartPosition; // enemy spawn
        private ContentManager _content; // store content
        private GraphicsDevice _graphicsDevice; // store graphics device

        public void Initialize(ContentManager content, GraphicsDevice graphicsDevice)
        {
            _content = content; // store content
            _graphicsDevice = graphicsDevice; // store graphics device
            _font = _content.Load<SpriteFont>("Fonts/Arial"); // load font for HP
            InitializeGameplay();
        }

        private void InitializeGameplay()
        {
            // initialize map
            map = new Map(_content, _graphicsDevice);

            // initialize hero
            Texture2D heroTexture = _content.Load<Texture2D>("tashfin");
            _startPosition = new Vector2(800 / 2 - 50, 480 / 2 - 50);
            hero = new Hero(heroTexture, _startPosition, null, "hero", 100f);
            InputManager inputManager = new InputManager(hero);
            hero.SetInputManager(inputManager);

            // initialize camera
            _camera = new Camera.Camera(_startPosition);

            // load swordman texture
            Texture2D swordmanTexture = _content.Load<Texture2D>("characters/lamtuni");
            _enemyStartPosition = new Vector2(100, 100);
            swordman = new Sahara_Swordsman(swordmanTexture, _enemyStartPosition, hero, "swordman", 80f);
        }

        public void Update(GameTime gameTime)
        {
            // update gameplay
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
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Begin();
            if (map != null)
            {
                var transformMatrix = _camera.GetTransformMatrix();
                spriteBatch.End();
                spriteBatch.Begin(transformMatrix: transformMatrix, samplerState: SamplerState.PointClamp);
                map.Draw(transformMatrix);
                hero.Draw(spriteBatch);
                swordman.Draw(spriteBatch);
                spriteBatch.End();
                spriteBatch.Begin(samplerState: SamplerState.PointClamp);
                // HP
                spriteBatch.DrawString(_font, $"Alive: {hero.HealthComponent.IsAlive}", new Vector2(10, 35), Color.White);
                spriteBatch.DrawString(_font, $"HP: {hero.HealthComponent.CurrentHealth}/{hero.HealthComponent.MaxHealth}", new Vector2(10, 10), Color.White);
                // restart
                if (!hero.HealthComponent.IsAlive)
                {
                    Vector2 youDiedPosition = new Vector2((830) / 2, (720) / 2);
                    Vector2 pressRPosition = new Vector2((730) / 2, (400));
                    spriteBatch.DrawString(_font, "You died!", youDiedPosition, Color.Red);
                    spriteBatch.DrawString(_font, "Press \"R\" to restart.", pressRPosition, Color.White);
                }
            }
            spriteBatch.End();
        }
    }
}