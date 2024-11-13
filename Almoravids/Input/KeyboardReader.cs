using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Almoravids.Input
{
    internal class KeyboardReader : IInputReader
    {
        public Vector2 ReadInput()
        {
            var direction = Vector2.Zero;

            KeyboardState state = Keyboard.GetState();
            // Check for diagonal movement by combining left/right with up/down
            if (state.IsKeyDown(Keys.Q) || state.IsKeyDown(Keys.Left))
            {
                direction.X = -1; // Move left
            }
            if (state.IsKeyDown(Keys.D) || state.IsKeyDown(Keys.Right))
            {
                direction.X = 1; // Move right
            }
            if (state.IsKeyDown(Keys.Z) || state.IsKeyDown(Keys.Up))
            {
                direction.Y = -1; // Move up
            }
            if (state.IsKeyDown(Keys.S) || state.IsKeyDown(Keys.Down))
            {
                direction.Y = 1; // Move down
            }

            // Normalize movement for consistent speed
            if (direction.Length() > 1)
            {
               direction.Normalize();
            }

            return direction;
        }
    }
}
