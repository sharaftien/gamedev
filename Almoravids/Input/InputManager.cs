using Almoravids.Interfaces;

namespace Almoravids.Input
{
    public class InputManager
    {
        private readonly Dictionary<Keys, ICommand> _keyCommands;
        private readonly IControllable _controllable;
        private Vector2 _combinedDirection;

        public InputManager(IControllable controllable)
        {
            _keyCommands = new Dictionary<Keys, ICommand>();
            _controllable = controllable;
            _combinedDirection = Vector2.Zero;
        }

        public void AddCommand(Keys key, ICommand command)
        {
            _keyCommands[key] = command;
        }

        public void Update(GameTime gameTime)
        {
            _combinedDirection = Vector2.Zero;
            KeyboardState state = Keyboard.GetState();

            foreach (var keyCommand in _keyCommands)
            {
                if (state.IsKeyDown(keyCommand.Key))
                {
                    keyCommand.Value.Execute(gameTime);
                }
            }

            if (_combinedDirection != Vector2.Zero)
            {
                _combinedDirection.Normalize();
                _controllable.SetDirection(_combinedDirection);
            }
            else
            {
                _controllable.SetDirection(Vector2.Zero);
            }
        }

        public void AddDirection(Vector2 direction)
        {
            _combinedDirection += direction;
        }
    }
}
