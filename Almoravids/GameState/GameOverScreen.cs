using Almoravids.Interfaces;
using Almoravids.UI;
using Almoravids.Animation;
using Almoravids.ContentManagement;

namespace Almoravids.GameState
{
    public class GameOverScreen : IGameState
    {
        private SpriteFont _font;
        private GraphicsDevice _graphicsDevice;
        private ContentLoader _contentLoader;
        private AnimationComponent _deathAnimation;
        private List<ButtonRenderer> _buttons;
        private TextRenderer _youDiedText;
        private int _currentLevel;

        public GameOverScreen(int currentLevel)
        {
            _currentLevel = currentLevel;
        }

        public void Initialize(ContentManager content, GraphicsDevice graphicsDevice)
        {
            _graphicsDevice = graphicsDevice;
            _contentLoader = new ContentLoader(content);
            _font = _contentLoader.LoadSpriteFont("Fonts/Arial");

            Texture2D heroTexture = _contentLoader.LoadTexture2D("tashfin");
            IAnimation animationSetup = new HeroAnimation(heroTexture);
            animationSetup.DefineAnimations();
            _deathAnimation = new AnimationComponent(animationSetup, "hero");

            _youDiedText = new TextRenderer(_font, "YOU DIED", Color.Red, 2.5f, true, true, _graphicsDevice);

            _buttons = new List<ButtonRenderer>
            {
                new ButtonRenderer(_font, "Try Again", Color.Red, new Vector2(0, 120), _graphicsDevice, () =>
                {
                    GameStateManager.Instance.SetState(new GameplayScreen(_currentLevel));
                }),
                new ButtonRenderer(_font, "Choose Level", Color.Red, new Vector2(0, 190), _graphicsDevice, () =>
                {
                    GameStateManager.Instance.SetState(new LevelScreen());
                })
            };
        }

        public void Update(GameTime gameTime)
        {
            foreach (var b in _buttons)
                b.Update(Mouse.GetState());

            if (Keyboard.GetState().IsKeyDown(Keys.R))
            {
                GameStateManager.Instance.SetState(new GameplayScreen(_currentLevel));
            }

            _deathAnimation.Update(gameTime, Vector2.Zero, false);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Begin();
            _youDiedText.Draw(spriteBatch, 0f, -150f);
            foreach (var button in _buttons)
                button.Draw(spriteBatch);
            spriteBatch.End();

            // death animation
            spriteBatch.Begin(SpriteSortMode.Deferred, null, SamplerState.PointClamp, null, null, null, Matrix.CreateScale(2.25f));
            Vector2 animationPos = new Vector2(
                (_graphicsDevice.Viewport.Width - 64 * 2.25f) / 2 / 2.25f,
                (_graphicsDevice.Viewport.Height - 80 * 2.25f) / 2 / 2.25f - 5f);
            _deathAnimation.Draw(spriteBatch, animationPos);
            spriteBatch.End();
        }
    }
}
