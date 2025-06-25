
namespace Almoravids.GameState
{
    public class GameStateManager
    {
        private static GameStateManager _instance;
        private IGameState _currentState;

        private GameStateManager() { }

        public static GameStateManager Instance
        {
            get { return _instance ??= new GameStateManager(); }
        }

        public void SetState(IGameState state, ContentManager content, GraphicsDevice graphicsDevice)
        {
            _currentState = state;
            _currentState.Initialize(content, graphicsDevice);
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