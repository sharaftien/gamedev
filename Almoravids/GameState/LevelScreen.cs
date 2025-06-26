
namespace Almoravids.GameState
{
    public class LevelScreen : IGameState
    {
        private SpriteFont _font;
        private SpriteFont _arialFont; // added for HP text
        private Map map;
        private Hero hero;
        private Sahara_Swordsman swordman;
        private Camera.Camera _camera;
        private Vector2 _startPosition; // hero spawn
        private Vector2 _enemyStartPosition; // enemy spawn
        private bool _levelSelected;
        private int _selectedLevel; // store selected level (1, 2, or 3)
        private ContentManager _content; // store content
        private GraphicsDevice _graphicsDevice; // store graphics device

        public void Initialize(ContentManager content, GraphicsDevice graphicsDevice)
        {
            _content = content; // store content
            _graphicsDevice = graphicsDevice; // store graphics device
            _font = _content.Load<SpriteFont>("Fonts/Arial");
            _arialFont = _content.Load<SpriteFont>("Fonts/Arial"); // load font for HP
            _levelSelected = false;
            _selectedLevel = 0;
            Console.WriteLine("LevelScreen initialized");
        }

        public void Update(GameTime gameTime)
        {
            if (!_levelSelected)
            {
                if (Keyboard.GetState().IsKeyDown(Keys.D1))
                {
                    _levelSelected = true;
                    _selectedLevel = 1;
                    InitializeGameplay();
                    Console.WriteLine("Level 1 selected");
                }
                if (Keyboard.GetState().IsKeyDown(Keys.D2))
                {
                    _levelSelected = true;
                    _selectedLevel = 2;
                    Console.WriteLine("Level 2 selected");
                }
                if (Keyboard.GetState().IsKeyDown(Keys.D3))
                {
                    _levelSelected = true;
                    _selectedLevel = 3;
                    Console.WriteLine("Level 3 selected");
                }
                if (Keyboard.GetState().IsKeyDown(Keys.Escape))
                {
                    Environment.Exit(0);
                }
                return;
            }

            if (_selectedLevel == 1)
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

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Begin();
            if (!_levelSelected)
            {
                spriteBatch.DrawString(_font, "Press 1, 2, 3 to select", new Vector2(310, 200), Color.White);
                spriteBatch.DrawString(_font, "Level 1", new Vector2(400, 300), Color.White);
                spriteBatch.DrawString(_font, "Level 2", new Vector2(400, 330), Color.White);
                spriteBatch.DrawString(_font, "Level 3", new Vector2(400, 360), Color.White);
            }
            else
            {
                if (_selectedLevel == 1)
                {
                    spriteBatch.DrawString(_font, "Loading Level...", new Vector2(350, 300), Color.White);
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
                        spriteBatch.DrawString(_arialFont, $"Alive: {hero.HealthComponent.IsAlive}", new Vector2(10, 35), Color.White);
                        spriteBatch.DrawString(_arialFont, $"HP: {hero.HealthComponent.CurrentHealth}/{hero.HealthComponent.MaxHealth}", new Vector2(10, 10), Color.White);
                        // restart
                        if (!hero.HealthComponent.IsAlive)
                        {
                            Vector2 youDiedPosition = new Vector2((830) / 2, (720) / 2);
                            Vector2 pressRPosition = new Vector2((730) / 2, (400));
                            spriteBatch.DrawString(_arialFont, "You died!", youDiedPosition, Color.Red);
                            spriteBatch.DrawString(_arialFont, "Press \"R\" to restart.", pressRPosition, Color.White);
                        }
                    }
                }
                else if (_selectedLevel == 2)
                {
                    spriteBatch.DrawString(_font, "Level 2 (Placeholder)", new Vector2(350, 300), Color.White);
                }
                else if (_selectedLevel == 3)
                {
                    spriteBatch.DrawString(_font, "Level 3 (Placeholder)", new Vector2(350, 300), Color.White);
                }
            }
            spriteBatch.End();
        }
    }
}