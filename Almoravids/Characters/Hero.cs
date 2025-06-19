using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Almoravids.Animation;
using Almoravids.Interfaces;
using System.Threading;
using SharpDX.DXGI;
using System;
using Microsoft.Xna.Framework.Input;
using Almoravids.Input;


namespace Almoravids.Characters
{
    public class Hero : Character
    {
        private InputManager _inputManager;

        public Hero(Texture2D texture, Vector2 startPosition, InputManager inputManager, string characterType = "hero", float speed = 100f)
            : base(texture, startPosition, characterType, speed)
        {
            _inputManager = inputManager;
            if (_inputManager != null)
            {
                ConfigureInput();
            }
        }

        public void SetInputManager(InputManager inputManager)
        {
            _inputManager = inputManager;
            ConfigureInput();
        }

        private void ConfigureInput()
        {
            _inputManager.AddCommand(Keys.Left, new MoveCommand(_inputManager, new Vector2(-1, 0)));
            _inputManager.AddCommand(Keys.Right, new MoveCommand(_inputManager, new Vector2(1, 0)));
            _inputManager.AddCommand(Keys.Up, new MoveCommand(_inputManager, new Vector2(0, -1)));
            _inputManager.AddCommand(Keys.Down, new MoveCommand(_inputManager, new Vector2(0, 1)));
            // wasd
            _inputManager.AddCommand(Keys.Q, new MoveCommand(_inputManager, new Vector2(-1, 0)));
            _inputManager.AddCommand(Keys.D, new MoveCommand(_inputManager, new Vector2(1, 0)));
            _inputManager.AddCommand(Keys.Z, new MoveCommand(_inputManager, new Vector2(0, -1)));
            _inputManager.AddCommand(Keys.S, new MoveCommand(_inputManager, new Vector2(0, 1)));
        }

        public override void Update(GameTime gameTime)
        {
            _inputManager.Update(gameTime);
            base.Update(gameTime);
        }
    }
}