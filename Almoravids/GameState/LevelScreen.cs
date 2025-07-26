using Almoravids.Interfaces;
using Almoravids.UI;
using Almoravids.ContentManagement;

namespace Almoravids.GameState
{
    public class LevelScreen : IGameState
    {
        private SpriteFont _font;
        private GraphicsDevice _graphicsDevice;
        private ContentLoader _contentLoader;
        private Texture2D _background;
        private List<ButtonRenderer> _buttons;

        public void Initialize(ContentManager content, GraphicsDevice graphicsDevice)
        {
            _graphicsDevice = graphicsDevice;
            _contentLoader = new ContentLoader(content);

            _font = _contentLoader.LoadSpriteFont("Fonts/Arial");
            _background = _contentLoader.LoadTexture2D("screens/finished");

            _buttons = new List<ButtonRenderer>
            {
                new ButtonRenderer(_font, "Sahara", Color.NavajoWhite, new Vector2(-230+16*4, 127+16*3), _graphicsDevice, () => // -228, 127 corner check
                {
                    GameStateManager.Instance.SetState(new GameplayScreen(1));
                }),
                new ButtonRenderer(_font, "Marrakech", Color.NavajoWhite, new Vector2(-88+16*5, -47), _graphicsDevice, () =>
                {
                    GameStateManager.Instance.SetState(new GameplayScreen(2));
                }),
                new ButtonRenderer(_font, "Strait of Gibraltar", Color.NavajoWhite, new Vector2(-24+16*9, -143), _graphicsDevice, () =>
                {
                    GameStateManager.Instance.SetState(new GameplayScreen(3));
                }),
                new ButtonRenderer(_font, "Return", Color.Red, new Vector2(-406+16*4, -251), _graphicsDevice, () =>
                {
                    GameStateManager.Instance.SetState(new StartScreen());
                })
            };
        }

        public void Update(GameTime gameTime)
        {
            var mouse = Mouse.GetState();
            foreach (var b in _buttons)
                b.Update(mouse);
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