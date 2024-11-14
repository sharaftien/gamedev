using Almoravids.Animation;
using Almoravids.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Almoravids.Characters
{
    public enum Direction
    {
        Up, Down, Left, Right
    }
    public abstract class Character : IGameObject
    {
        protected Texture2D idleTexture;
        protected Texture2D walkTexture;
        protected Animate animate;
        protected Vector2 position;
        protected Direction currentDirection;

        protected bool isMoving = false;

        public Character(Texture2D idleTexture, Texture2D walkTexture, Vector2 startPosition)
        {
            this.idleTexture = idleTexture;
            this.walkTexture = walkTexture;
            this.position = startPosition;

            // Initialize the animation
            animate = new Animate();
            SetupAnimations();
        }

        private void SetupAnimations()  //load frames from the texture
        {
            // walking animations are 8 sprites not counting first static sprite
            // TO BE LOOPED

            //WALKING DOWN (front)
            for (int i = 0; i < 8; i++)
            {
                animate.AddFrame(Direction.Down, new AnimationFrame(new Rectangle(i * 64, 64*10, 64, 64)));
            }

            //WALKING UP (back)
            for (int i = 0; i < 8; i++)
            {
                animate.AddFrame(Direction.Up, new AnimationFrame(new Rectangle(i * 64, 64*8, 64, 64)));
            }

            //WALKING LEFT
            for (int i = 0; i < 8; i++)
            {
                animate.AddFrame(Direction.Left, new AnimationFrame(new Rectangle(i * 64, 64*9, 64, 64)));
            }

            //WALKING RIGHT
            for (int i = 0; i < 8; i++)
            {
                animate.AddFrame(Direction.Right, new AnimationFrame(new Rectangle(i * 64, 64*11, 64, 64)));
            }


        }
        public virtual void Update(GameTime gameTime)  // Required by IGameObject interface
        {
            // Can be overridden in inherited classes (hero class bvb)
        }

        // Adds direction-based movement for derived classes
        public virtual void Move(Vector2 direction, GameTime gameTime)
        {
            isMoving = direction != Vector2.Zero;
            if (isMoving)
            {

                // determine direction
                if (direction.Y < 0)
                {
                    currentDirection = Direction.Up;
                }
                if (direction.Y > 0)
                {
                    currentDirection = Direction.Down;
                }
                if (direction.X < 0)
                {
                    currentDirection = Direction.Left;
                }
                if (direction.X > 0)
                {
                    currentDirection = Direction.Right;
                }

                position += direction * 2; // *2 -> twice as fast
                animate.Update(gameTime, currentDirection); // Pass gameTime to Animate.Update                
            }


            
        }

        public virtual void Draw(SpriteBatch spriteBatch) //renders correct animation frame (idle when standing, walking when moving)
        {
            // Choose the texture based on the character’s state
            Texture2D currentTexture = isMoving ? walkTexture : idleTexture;
            spriteBatch.Draw(currentTexture, position, animate.CurrentFrame.SourceRectangle, Color.White);
        }
    }
}
