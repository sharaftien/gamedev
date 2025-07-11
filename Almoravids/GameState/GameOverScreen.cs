
namespace Almoravids.GameState
{
    public class GameOverScreen : IGameState
    {
        private SpriteFont _font;
        private AnimationComponent _deathAnimation;
        private GraphicsDevice _graphicsDevice;
        private ContentLoader _contentLoader;
        private List<TextRenderer> _textRenderers;

        public void Initialize(ContentManager content, GraphicsDevice graphicsDevice)
        {
            _graphicsDevice = graphicsDevice;
            _contentLoader = new ContentLoader(content);
            _font = _contentLoader.LoadSpriteFont("Fonts/Arial"); // load font for UI
            Texture2D heroTexture = _contentLoader.LoadTexture2D("tashfin");
            _deathAnimation = new AnimationComponent(heroTexture, "hero");
            _deathAnimation.SetAnimation("death");

            _textRenderers = new List<TextRenderer>
            {
                new TextRenderer(_font, "You died!", Color.Red, 2.5f, true, true, _graphicsDevice),
                new TextRenderer(_font, "Press [R] to restart.", Color.White, 1f, true, true, _graphicsDevice),
                new TextRenderer(_font, "Press [Enter] to return to menu.", Color.White, 1f, true, true, _graphicsDevice)
            };
        }

        public void Update(GameTime gameTime)
        {
            // handle input for restart or menu
            if (Keyboard.GetState().IsKeyDown(Keys.R))
            {
                GameStateManager.Instance.SetState(new GameplayScreen());
            }
            if (Keyboard.GetState().IsKeyDown(Keys.Enter))
            {
                GameStateManager.Instance.SetState(new StartScreen());
            }

            // hero death animation
            _deathAnimation.Update(gameTime, Vector2.Zero);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Begin();

            _textRenderers[0].Draw(spriteBatch, 0f, -120f); // "You died!" (up)
            _textRenderers[1].Draw(spriteBatch, 0f, 100f); // "Press [R]" (down)
            _textRenderers[2].Draw(spriteBatch, 0f, 150f); // "Press [Enter]" (further down)
            spriteBatch.End();

            spriteBatch.Begin(SpriteSortMode.Deferred, null, SamplerState.PointClamp, null, null, null, Matrix.CreateScale(1.5f));
            Vector2 animationPos = new Vector2(
                (_graphicsDevice.Viewport.Width - 64 * 1.5f) / 2 / 1.5f,
                (_graphicsDevice.Viewport.Height - 64 * 1.5f) / 2 / 1.5f - 5f);
            _deathAnimation?.Draw(spriteBatch, animationPos);
            spriteBatch.End();
        }
    }
}
