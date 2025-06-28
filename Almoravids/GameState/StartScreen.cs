
namespace Almoravids.GameState
{
    public class StartScreen : IGameState
    {
        private SpriteFont _font;
        private ContentLoader _contentLoader; // store content loader

        public void Initialize(ContentManager content, GraphicsDevice graphicsDevice)
        {
            _contentLoader = new ContentLoader(content); // initialize content loader
            _font = _contentLoader.LoadSpriteFont("Fonts/Arial");
            Console.WriteLine("StartScreen initialized");
        }

        public void Update(GameTime gameTime)
        {
            if (Keyboard.GetState().IsKeyDown(Keys.Enter))
            {
                GameStateManager.Instance.SetState(new LevelScreen());
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Begin();
            spriteBatch.DrawString(_font, "ALMORAVIDS", new Vector2(300, 250), Color.Gold, 0f, Vector2.Zero, 2f, SpriteEffects.None, 0f);
            spriteBatch.DrawString(_font, "Press Enter to start", new Vector2(340, 350), Color.White);
            spriteBatch.End();
        }
    }
}