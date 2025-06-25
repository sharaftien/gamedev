
namespace Almoravids.GameState
{
    public class StartScreen : IGameState
    {
        private SpriteFont _font;
        private SpriteFont _Titlefont;
        private bool _enterPressed;

        public void Initialize(ContentManager content, GraphicsDevice graphicsDevice)
        {
            _font = content.Load<SpriteFont>("Fonts/Arial");
            _enterPressed = false;
            Console.WriteLine("StartScreen initialized");
        }

        public void Update(GameTime gameTime)
        {
            if (Keyboard.GetState().IsKeyDown(Keys.Enter))
            {
                _enterPressed = true;
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Begin();
            if (!_enterPressed)
            {
                spriteBatch.DrawString(_font, "ALMORAVIDS", new Vector2(300, 250), Color.Gold, 0f, Vector2.Zero, 2f, SpriteEffects.None, 0f);
                spriteBatch.DrawString(_font, "Press Enter to start", new Vector2(340, 350), Color.White);
            }
            else
            {
                spriteBatch.DrawString(_font, "Starting...", new Vector2(420, 300), Color.White);
            }
            spriteBatch.End();
        }
    }
}