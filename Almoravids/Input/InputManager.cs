
namespace Almoravids.Input
{
    public class InputManager
    {
        private readonly Dictionary<Keys, ICommand> _keyCommands;
        private readonly Character _character;
        private Vector2 _combinedDirection;

        public InputManager(Character character)
        {
            _keyCommands = new Dictionary<Keys, ICommand>();
            _character = character;
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
                _character.MovementComponent.SetDirection(_combinedDirection);
            }
            else
            {
                _character.MovementComponent.SetDirection(Vector2.Zero);
            }
        }

        public void AddDirection(Vector2 direction)
        {
            _combinedDirection += direction;
        }
    }
}
