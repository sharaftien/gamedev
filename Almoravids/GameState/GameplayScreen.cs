
namespace Almoravids.GameState
{
    public class GameplayScreen : IGameState
    {
        private Map map;
        private Hero hero;
        private List<Sahara_Swordsman> swordsmen; // multiple enemies -> list
        private Camera.Camera _camera;
        private SpriteFont _font; // for HP text
        private Vector2 _startPosition; // hero spawn
        private List<Vector2> _enemyStartPositions; // enemy spawn
        private ContentManager _content; // store content
        private GraphicsDevice _graphicsDevice; // store graphics device
        private int _level; // store selected level
        private ContentLoader _contentLoader; // store content loader

        public GameplayScreen(int level = 1)
        {
            _level = level; // set level
        }

        public void Initialize(ContentManager content, GraphicsDevice graphicsDevice)
        {
            _content = content; // store content
            _graphicsDevice = graphicsDevice; // store graphics device
            _contentLoader = new ContentLoader(content); // initialize content loader
            _font = _contentLoader.LoadSpriteFont("Fonts/Arial"); // load font for HP
            InitializeGameplay();
        }

        private void InitializeGameplay()
        {
            Level.Level level;

            switch (_level)
            {
                case 2:
                    level = new Level_2(_contentLoader, _graphicsDevice);
                    break;

                case 3:
                    level = new Level_1(_contentLoader, _graphicsDevice);
                    break;

                case 1:
                default:
                    level = new Level_1(_contentLoader, _graphicsDevice);
                    break;
            }

            level.Load();
            map = level.Map;
            _startPosition = level.HeroSpawn;
            _enemyStartPositions = level.EnemySpawns;

            // initialize hero
            Texture2D heroTexture = _contentLoader.LoadTexture2D("tashfin");
            hero = new Hero(heroTexture, _startPosition, null, "hero", 100f);
            InputManager inputManager = new InputManager(hero);
            hero.SetInputManager(inputManager);

            // initialize camera
            _camera = new Camera.Camera(_startPosition);

            // initialize enemies
            Texture2D swordmanTexture = _contentLoader.LoadTexture2D("characters/lamtuni");
            swordsmen = new List<Sahara_Swordsman>();
            foreach (var pos in _enemyStartPositions)
            {
                swordsmen.Add(new Sahara_Swordsman(swordmanTexture, pos, hero, "swordman", 80f));
            }
        }

        public void Update(GameTime gameTime)
        {
            // update gameplay
            map.Update(gameTime);

            // update hero
            Vector2 heroProposedPosition = hero.MovementComponent.Position;
            hero.Update(gameTime);

            // update enemies
            List<Vector2> swordsmenProposedPositions = swordsmen.Select(s => s.MovementComponent.Position).ToList();
            foreach (var swordman in swordsmen)
            {
                swordman.Update(gameTime);
            }

            // check map collisions
            if (hero.CollisionComponent.CheckMapCollision(map.CollisionLayer, out Vector2 heroMapResolution))
            {
                hero.MovementComponent.Position = heroProposedPosition + heroMapResolution;
                hero.CollisionComponent.Update(hero.MovementComponent.Position);
            }

            // Check map collisions for enemies
            for (int i = 0; i < swordsmen.Count; i++)
            {
                if (swordsmen[i].CollisionComponent.CheckMapCollision(map.CollisionLayer, out Vector2 swordmanMapResolution))
                {
                    swordsmen[i].MovementComponent.Position = swordsmenProposedPositions[i] + swordmanMapResolution;
                    swordsmen[i].CollisionComponent.Update(swordsmen[i].MovementComponent.Position);
                }
            }

            // Check enemy collisions with hero
            if (hero.HealthComponent.IsAlive)
            {
                foreach (var swordman in swordsmen)
                {
                    if (hero.CollisionComponent.BoundingBox.Intersects(swordman.CollisionComponent.BoundingBox))
                    {
                        Vector2 knockbackDirection = hero.MovementComponent.Position - swordman.MovementComponent.Position;
                        hero.HealthComponent.TakeDamage(1, knockbackDirection);
                    }
                }
            }

            // Check for restart
            if (!hero.HealthComponent.IsAlive && Keyboard.GetState().IsKeyDown(Keys.R))
            {
                hero.Reset(_startPosition);
                for (int i = 0; i < swordsmen.Count; i++)
                {
                    swordsmen[i].Reset(_enemyStartPositions[i]);
                }
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
                foreach (var swordman in swordsmen)
                {
                    swordman.Draw(spriteBatch);
                }
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