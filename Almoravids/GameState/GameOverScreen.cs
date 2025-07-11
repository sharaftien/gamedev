
namespace Almoravids.GameState
{
    public class GameOverScreen : IGameState
    {
        private SpriteFont _font;
        private ContentLoader _contentLoader;

        public void Initialize(ContentManager content, GraphicsDevice graphicsDevice)
        {
            _contentLoader = new ContentLoader(content);
            _font = _contentLoader.LoadSpriteFont("Fonts/Arial"); // load font for UI
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
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Begin();
            Vector2 youDiedPosition = new Vector2((830) / 2, (720) / 2);
            Vector2 pressRPosition = new Vector2((730) / 2, (400));
            Vector2 pressEnterPosition = new Vector2(730 / 2-100, 450);
            spriteBatch.DrawString(_font, "You died!", youDiedPosition, Color.Red);
            spriteBatch.DrawString(_font, "Press \"R\" to restart.", pressRPosition, Color.White);
            spriteBatch.DrawString(_font, "Press \"Enter\" to return to menu.", pressEnterPosition, Color.White);
            spriteBatch.End();
        }
    }
}