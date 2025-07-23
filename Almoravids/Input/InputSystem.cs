
namespace Almoravids.Input
{
    public class InputSystem // input manager for screens/characters
    {
        private readonly List<InputManager> _inputManagers;

        public InputSystem()
        {
            _inputManagers = new List<InputManager>(); 
        }

        public void AddInputManager(InputManager inputManager, IControllable controllable)
        {
            _inputManagers.Add(inputManager);

            inputManager.AddCommand(Keys.Left, new MoveCommand(inputManager, new Vector2(-1, 0)));
            inputManager.AddCommand(Keys.Right, new MoveCommand(inputManager, new Vector2(1, 0)));
            inputManager.AddCommand(Keys.Up, new MoveCommand(inputManager, new Vector2(0, -1)));
            inputManager.AddCommand(Keys.Down, new MoveCommand(inputManager, new Vector2(0, 1)));
            // wasd
            inputManager.AddCommand(Keys.Q, new MoveCommand(inputManager, new Vector2(-1, 0)));
            inputManager.AddCommand(Keys.D, new MoveCommand(inputManager, new Vector2(1, 0)));
            inputManager.AddCommand(Keys.Z, new MoveCommand(inputManager, new Vector2(0, -1)));
            inputManager.AddCommand(Keys.S, new MoveCommand(inputManager, new Vector2(0, 1)));
        }

        public void Update(GameTime gameTime)
        {
            foreach (var inputManager in _inputManagers)
            {
                inputManager.Update(gameTime);
            }
        }
    }
}