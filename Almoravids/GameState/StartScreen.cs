
namespace Almoravids.GameState
{
    public class StartScreen : IGameState
    {
        private SpriteFont _font;
        private ContentLoader _contentLoader;
        private GraphicsDevice _graphicsDevice;
        private List<TextRenderer> _textRenderers;
        private Texture2D _background;

        public void Initialize(ContentManager content, GraphicsDevice graphicsDevice)
        {
            _graphicsDevice = graphicsDevice;
            _contentLoader = new ContentLoader(content);
            _font = _contentLoader.LoadSpriteFont("Fonts/Arial");
            _background = _contentLoader.LoadTexture2D("screens/title");

            _textRenderers = new List<TextRenderer>
            {
                new TextRenderer(_font, "", Color.Gold, 2f, true, true, _graphicsDevice),
                new TextRenderer(_font, "Press [Enter] to continue", Color.White, 1f, true, true, _graphicsDevice)
            };
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
            spriteBatch.Draw(_background, Vector2.Zero, Color.White);
            _textRenderers[0].Draw(spriteBatch, 0f, -50f); // "ALMORAVID"
            _textRenderers[1].Draw(spriteBatch, 0f, 50f); // "Level Select"
            spriteBatch.End();
        }
    }
}