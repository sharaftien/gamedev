using Almoravids.Interfaces;

namespace Almoravids.GameState
{
    public class GameStateManager
    {
        private static GameStateManager _instance;
        private IGameState _currentState;
        private ContentManager _content;
        private GraphicsDevice _graphicsDevice;

        private GameStateManager() { }

        public static GameStateManager Instance
        {
            get { return _instance ??= new GameStateManager(); }
        }

        public void Initialize(ContentManager content, GraphicsDevice graphicsDevice)
        {
            _content = content;
            _graphicsDevice = graphicsDevice;
        }

        public void SetState(IGameState state)
        {
            _currentState = state;
            _currentState.Initialize(_content, _graphicsDevice);
            Console.WriteLine("State set to: " + state.GetType().Name);
        }

        public void Update(GameTime gameTime)
        {
            _currentState?.Update(gameTime);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            _currentState?.Draw(spriteBatch);
        }
    }
}