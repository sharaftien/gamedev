
namespace Almoravids.GameState
{
    public class LevelScreen : IGameState
    {
        private SpriteFont _font;
        private bool _levelSelected;

        public void Initialize(ContentManager content, GraphicsDevice graphicsDevice)
        {
            _font = content.Load<SpriteFont>("Fonts/Arial");
            _levelSelected = false;
            Console.WriteLine("LevelScreen initialized");
        }

        public void Update(GameTime gameTime)
        {
            if (!_levelSelected)
            {
                if (Keyboard.GetState().IsKeyDown(Keys.D1))
                {
                    _levelSelected = true;
                }
                if (Keyboard.GetState().IsKeyDown(Keys.D2))
                {
                    _levelSelected = true;
                }
                if (Keyboard.GetState().IsKeyDown(Keys.D3))
                {
                    _levelSelected = true;
                }
                if (Keyboard.GetState().IsKeyDown(Keys.Escape))
                {
                    Environment.Exit(0);
                }
                return;
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
            else
            {
                spriteBatch.DrawString(_font, "Loading Level...", new Vector2(350, 300), Color.White);
            }
            spriteBatch.End();
        }
    }
}