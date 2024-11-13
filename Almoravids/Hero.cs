using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Almoravids.Animation;
using Almoravids.Interfaces;
using System.Threading;
using SharpDX.DXGI;
using System;
using Microsoft.Xna.Framework.Input;
using Almoravids.Input;

namespace Almoravids
{
    public class Hero : IGameObject
    {
        private Texture2D heroTexture;
        private Animate animate;
        private Vector2 position;
        private Vector2 speed;
        private Vector2 acceleration;
        private Vector2 mouseVector;
        IInputReader inputReader;
        private bool isMoving;

        public Hero(Texture2D texture, IInputReader reader)
        {
            heroTexture = texture;      //texture when walking
            animate = new Animate();
            for (int i = 0; i < 8; i++)
            {
                animate.AddFrame(new AnimationFrame(new Rectangle((64 * i) + 64, 64 * 11, 64, 64)));
            }
            
            position = new Vector2(10, 10);
            speed = new Vector2(1, 1);
            acceleration = new Vector2(0.1f, 0.1f);

            //Read input for my hero class
            this.inputReader = reader;
        }

        public void Update(GameTime gameTime)
        {
            // get the moving direction (keyboard input)
            var direction = inputReader.ReadInput();
            direction *= 2; //speed (*=4 -> 4 times as fast)
            isMoving = direction != Vector2.Zero;   //boolean isMoving -> true when moving, false when still

            //update position (keyboard input)
            position += direction;

            if (isMoving)   //so only when moving the sprite will be animated
            {
                //Move(GetMouseState());
                animate.Update(gameTime);
            }
            
        }

        private Vector2 GetMouseState()
        {
            MouseState state = Mouse.GetState();
            mouseVector = new Vector2(state.X, state.Y);
            return mouseVector;
        }

        private void Move(Vector2 mouse)
        {
            var direction = Vector2.Add(mouse, -position);
            direction.Normalize();
            direction = Vector2.Multiply(direction, 0.2f);

            //position += speed;
            //speed += acceleration;
            //speed = Limit(speed, 5);

            speed += direction;
            speed = Limit(speed, 4);
            position += speed;

            if (position.X > 600 || position.X < 0)
            {
                speed.X *= -1;
                acceleration.X *= -1;
            }
            if (position.Y > 400 || position.Y < 0)
            {
                speed.Y *= -1;
                acceleration *= -1;
            }
        }

        private Vector2 Limit(Vector2 v, float max)
        {
            if (v.Length() > max)
            {
                var ratio = max / v.Length();
                v.X *= ratio;
                v.Y *= ratio;
            } return v;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(heroTexture, position, animate.CurrentFrame.SourceRectangle, Color.White, 0, new Vector2(0, 0), 1.5f, SpriteEffects.None, 0);
        }
    }
}
