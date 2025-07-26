using Almoravids.Interfaces;

namespace Almoravids.Input
{
    public class MoveCommand : ICommand
    {
        private readonly InputManager _inputManager;
        private readonly Vector2 _direction;

        public MoveCommand(InputManager inputManager, Vector2 direction)
        {
            _inputManager = inputManager;
            _direction = direction;
        }

        public void Execute(GameTime gameTime)
        {
            _inputManager.AddDirection(_direction);
        }
    }
}
