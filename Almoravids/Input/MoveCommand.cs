using Almoravids.Characters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
