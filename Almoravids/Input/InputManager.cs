using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Almoravids.Input
{
    public class InputManager
    {
        private readonly Dictionary<Keys, ICommand> _keyCommands;

        public InputManager()
        {
            _keyCommands = new Dictionary<Keys, ICommand>();
        }

        public void AddCommand(Keys key, ICommand command)
        {
            _keyCommands[key] = command;
        }

        public void Update(GameTime gameTime)
        {
            KeyboardState state = Keyboard.GetState();
            foreach (var keyCommand in _keyCommands)
            {
                if (state.IsKeyDown(keyCommand.Key))
                {
                    keyCommand.Value.Execute(gameTime);
                }
            }
        }
    }
}
