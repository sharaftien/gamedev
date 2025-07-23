
namespace Almoravids
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        private GameStateManager _gameStateManager;

        public static Texture2D Pixel { get; private set; }

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            _graphics.PreferredBackBufferWidth = 960;
            _graphics.PreferredBackBufferHeight = 720;
            _graphics.ApplyChanges();
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            Console.WriteLine($"Game compiling at {DateTime.Now}");
            base.Initialize();
        }

        protected override void LoadContent()
        {
            // pixel instead of loading texture
            Texture2D pixel = new Texture2D(GraphicsDevice, 1, 1);
            pixel.SetData(new[] { Color.White });
            Pixel = pixel;

            _spriteBatch = new SpriteBatch(GraphicsDevice);
            _gameStateManager = GameStateManager.Instance;
            _gameStateManager.Initialize(Content, GraphicsDevice);
            _gameStateManager.SetState(new GameState.StartScreen());
        }

        protected override void Update(GameTime gameTime)
        {
            if (Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            _gameStateManager.Update(gameTime);
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);
            _gameStateManager.Draw(_spriteBatch);
            base.Draw(gameTime);
        }
    }
}