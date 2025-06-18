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

        public Hero(Texture2D walkTexture, Texture2D idleTexture, Vector2 startPosition, IInputReader inputReader, string characterType = "hero")
            : base(walkTexture, idleTexture, startPosition, characterType)
        {
            this.inputReader = inputReader;
        }

        public override void Update(GameTime gameTime)
        {
            Vector2 direction = inputReader.ReadInput();
            Move(direction, gameTime);
        }
    }
}