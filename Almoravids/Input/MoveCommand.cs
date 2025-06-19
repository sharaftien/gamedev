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
        private readonly Character _character;
        private readonly Vector2 _direction;

        public MoveCommand(Character character, Vector2 direction)
        {
            _character = character;
            _direction = direction;
        }

        public void Execute(GameTime gameTime)
        {
            _character.MovementComponent.SetDirection(_direction);
        }
    }
}
