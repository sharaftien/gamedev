
namespace Almoravids.GameState
{
    public class StageClearedScreen : IGameState
    {
        private SpriteFont _font;
        private GraphicsDevice _graphicsDevice;
        private ContentLoader _contentLoader;
        private AnimationComponent _walkingAnimation;
        private List<ButtonRenderer> _buttons;
        private TextRenderer _BayaahText;
        private int _currentLevel;

        public StageClearedScreen(int currentLevel)
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
            _walkingAnimation = new AnimationComponent(animationSetup, "walk");

            _BayaahText = new TextRenderer(_font, "BAY'AH COLLECTED", Color.Green, 2.5f, true, true, _graphicsDevice);

            _buttons = new List<ButtonRenderer>
            {
                new ButtonRenderer(_font, "Continue", Color.Green, new Vector2(0, 100), _graphicsDevice, () =>
                {
                    GameStateManager.Instance.SetState(new LevelScreen());
                }),
                new ButtonRenderer(_font, "Play again", Color.Orange, new Vector2(0, 170), _graphicsDevice, () =>
                {
                    GameStateManager.Instance.SetState(new GameplayScreen(_currentLevel));
                }),
                new ButtonRenderer(_font, "Go to Start", Color.Orange, new Vector2(0, 240), _graphicsDevice, () =>
                {
                    GameStateManager.Instance.SetState(new StartScreen());
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

            _walkingAnimation.Update(gameTime, Vector2.Zero, true); // true for isAlive
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Begin();
            _BayaahText.Draw(spriteBatch, 0f, -210);
            foreach (var button in _buttons)
                button.Draw(spriteBatch);
            spriteBatch.End();

            spriteBatch.Begin(SpriteSortMode.Deferred, null, SamplerState.PointClamp, null, null, null, Matrix.CreateScale(2.25f));
            Vector2 animationPos = new Vector2(
                (_graphicsDevice.Viewport.Width - 64 * 2.25f) / 2 / 2.25f,
                (_graphicsDevice.Viewport.Height - (80 * 2.25f) - 60) / 2 / 2.25f - 5f);
            _walkingAnimation.Draw(spriteBatch, animationPos);
            spriteBatch.End();
        }
    }
}