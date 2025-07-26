using Almoravids.UI;
using Almoravids.ContentManagement;
using Almoravids.Interfaces;

namespace Almoravids.GameState
{
    public class StartScreen : IGameState
    {
        private SpriteFont _font;
        private ContentLoader _contentLoader;
        private GraphicsDevice _graphicsDevice;
        private Texture2D _background;
        private List<ButtonRenderer> _buttons;

        public void Initialize(ContentManager content, GraphicsDevice graphicsDevice)
        {
            _graphicsDevice = graphicsDevice;
            _contentLoader = new ContentLoader(content);

            _font = _contentLoader.LoadSpriteFont("Fonts/Arial");
            _background = _contentLoader.LoadTexture2D("screens/title");

            _buttons = new List<ButtonRenderer>
            {
                new ButtonRenderer(_font, "Start Game", Color.Orange, new Vector2(0, 35), _graphicsDevice, () =>
                {
                    GameStateManager.Instance.SetState(new LevelScreen());
                }),
                new ButtonRenderer(_font, "Quit", Color.Red, new Vector2(0, 100), _graphicsDevice, () =>
                {
                    Environment.Exit(0);
                })
            };
        }

        public void Update(GameTime gameTime)
        {
            var mouse = Mouse.GetState();
            foreach (var button in _buttons)
                button.Update(mouse);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Begin();
            spriteBatch.Draw(_background, Vector2.Zero, Color.White);

            foreach (var button in _buttons)
                button.Draw(spriteBatch);

            spriteBatch.End();
        }
    }
}