
namespace Almoravids.GameState
{
    public class LevelScreen : IGameState
    {
        private SpriteFont _font;
        private bool _levelSelected;
        private int _selectedLevel; // store selected level (1, 2, or 3)
        private ContentManager _content; // store content
        private GraphicsDevice _graphicsDevice; // store graphics device
        private ContentLoader _contentLoader; // store content loader

        public void Initialize(ContentManager content, GraphicsDevice graphicsDevice)
        {
            _content = content; // store content
            _graphicsDevice = graphicsDevice; // store graphics device
            _contentLoader = new ContentLoader(content); // initialize content loader
            _font = _contentLoader.LoadSpriteFont("Fonts/Arial");
            _levelSelected = false;
            _selectedLevel = 0;
            Console.WriteLine("LevelScreen initialized");
        }

        public void Update(GameTime gameTime)
        {
            if (!_levelSelected)
            {
                if (Keyboard.GetState().IsKeyDown(Keys.D1)|| Keyboard.GetState().IsKeyDown(Keys.NumPad1))
                {
                    _levelSelected = true;
                    _selectedLevel = 1;
                    GameStateManager.Instance.SetState(new GameplayScreen(1));
                }
                if (Keyboard.GetState().IsKeyDown(Keys.D2) || Keyboard.GetState().IsKeyDown(Keys.NumPad2))
                {
                    _levelSelected = true;
                    _selectedLevel = 2;
                    GameStateManager.Instance.SetState(new GameplayScreen(2));
                }
                if (Keyboard.GetState().IsKeyDown(Keys.D3) || Keyboard.GetState().IsKeyDown(Keys.NumPad3))
                {
                    _levelSelected = true;
                    _selectedLevel = 3;
                    GameStateManager.Instance.SetState(new GameplayScreen(3));
                }
                if (Keyboard.GetState().IsKeyDown(Keys.Escape))
                {
                    Environment.Exit(0);
                }
            }
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
            else if (_selectedLevel == 2)
            {
                spriteBatch.DrawString(_font, "Level 2 (Marrakech)", new Vector2(350, 300), Color.White);
            }
            else if (_selectedLevel == 3)
            {
                spriteBatch.DrawString(_font, "Level 3 (Placeholder)", new Vector2(350, 300), Color.White);
            }
            spriteBatch.End();
        }
    }
}