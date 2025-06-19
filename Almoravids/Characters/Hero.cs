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
        private readonly IInputReader inputReader;

        public Hero(Texture2D texture, Vector2 startPosition, IInputReader inputReader, string characterType = "hero", float speed = 100f)
            : base(texture, startPosition, characterType, speed)
        {
            this.inputReader = inputReader;
        }

        public override void Update(GameTime gameTime)
        {
            Vector2 inputDirection = inputReader.ReadInput();
            MovementComponent.SetDirection(inputDirection);
            base.Update(gameTime);
        }
    }
}