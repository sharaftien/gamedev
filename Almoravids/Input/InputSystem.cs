
namespace Almoravids.Input
{
    public class InputSystem // input manager for screens/characters
    {
        private readonly List<InputManager> _inputManagers;

        public InputSystem()
        {
            _inputManagers = new List<InputManager>(); 
        }

        public void AddInputManager(InputManager inputManager)
        {
            _inputManagers.Add(inputManager);
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