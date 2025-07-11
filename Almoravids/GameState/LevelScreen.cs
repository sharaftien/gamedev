
namespace Almoravids.GameState
{
    public class LevelScreen : IGameState
    {
        private SpriteFont _font;
        private GraphicsDevice _graphicsDevice;
        private ContentLoader _contentLoader;
        private List<TextRenderer> _textRenderers;

        public void Initialize(ContentManager content, GraphicsDevice graphicsDevice)
        {
            _graphicsDevice = graphicsDevice;
            _contentLoader = new ContentLoader(content);
            _font = _contentLoader.LoadSpriteFont("Fonts/Arial");

            // initialize text renderers
            _textRenderers = new List<TextRenderer>
            {
                new TextRenderer(_font, "Press [1] or [2] to select level", Color.White, 1.3f, true, true, _graphicsDevice),
                new TextRenderer(_font, "Press [R] to return", Color.White, 1.3f, true, true, _graphicsDevice),
                new TextRenderer(_font, "Level 1", Color.White, 1f, true, true, _graphicsDevice),
                new TextRenderer(_font, "Level 2", Color.White, 1f, true, true, _graphicsDevice),
                new TextRenderer(_font, "Level 3", Color.White, 1f, true, true, _graphicsDevice),
                new TextRenderer(_font, "Return", Color.White, 1f, true, true, _graphicsDevice),
            };
        }

        public void Update(GameTime gameTime)
        {
            if (Keyboard.GetState().IsKeyDown(Keys.D1)|| Keyboard.GetState().IsKeyDown(Keys.NumPad1))
            {
                GameStateManager.Instance.SetState(new GameplayScreen(1));
            }
            if (Keyboard.GetState().IsKeyDown(Keys.D2) || Keyboard.GetState().IsKeyDown(Keys.NumPad2))
            {
                GameStateManager.Instance.SetState(new GameplayScreen(2));
            }
            if (Keyboard.GetState().IsKeyDown(Keys.D3) || Keyboard.GetState().IsKeyDown(Keys.NumPad3))
            {
                GameStateManager.Instance.SetState(new GameplayScreen(1));
            }
            if (Keyboard.GetState().IsKeyDown(Keys.R))
            {
                GameStateManager.Instance.SetState(new StartScreen());
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Begin();
            _textRenderers[0].Draw(spriteBatch, 0f, -200f);
            _textRenderers[1].Draw(spriteBatch, 0f, -150f);
            _textRenderers[2].Draw(spriteBatch, 0f, 0f);
            _textRenderers[3].Draw(spriteBatch, 0f, 40f);
            _textRenderers[4].Draw(spriteBatch, 0f, 80f);
            _textRenderers[5].Draw(spriteBatch, 0f, 200f);
            spriteBatch.End();
        }
    }
}