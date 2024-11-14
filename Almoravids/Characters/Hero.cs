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
    public class Hero : Character, IGameObject
    {
        private readonly IInputReader inputReader;

        public Hero(Texture2D idleTexture, Texture2D walkTexture, Vector2 startPosition, IInputReader inputReader)
            : base(idleTexture, walkTexture, startPosition)
        {
            this.inputReader = inputReader;
        }

        public override void Update(GameTime gameTime)
        {
            // Read input for movement direction
            Vector2 direction = inputReader.ReadInput();
            Move(direction, gameTime); // Call base.Move with gameTime

            if (isMoving)
            {
                walkAnimate.Update(gameTime, currentDirection);
            }
            else
            {
                idleAnimate.Update(gameTime, currentDirection);
            }
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            //// Draw hero using inherited Draw method
            //base.Draw(spriteBatch);
            Texture2D currentTexture = isMoving ? walkTexture : idleTexture;
            AnimationFrame currentFrame = isMoving ? walkAnimate.CurrentFrame : idleAnimate.CurrentFrame;

            spriteBatch.Draw(currentTexture, position, currentFrame.SourceRectangle, Color.White);
        }

    }
}
