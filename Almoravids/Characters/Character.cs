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
        protected Animate walkAnimate;
        protected Animate idleAnimate;
        protected Vector2 position;
        protected Direction currentDirection;

        protected bool isMoving = false;

        public Character(Texture2D idleTexture, Texture2D walkTexture, Vector2 startPosition)
        {
            this.idleTexture = idleTexture;
            this.walkTexture = walkTexture;
            this.position = startPosition;

            //hero looks down when game starts
            currentDirection = Direction.Down;

            // Initialize the animation
            walkAnimate = new Animate(0.15);     //standard speed
            idleAnimate = new Animate(3);    //still a bit weird, to be changed
            SetupWalkAnimations();
            SetupIdleAnimations();
        }

        private void SetupIdleAnimations() //load frames from the idle texture (tashfin_idle)
        {
            //idle animations (to be looped aswell) (two frames only)
            
            //LOOKING UP (back)
            for (int i = 0; i < 2; i++)
            {
                idleAnimate.AddFrame(Direction.Up, new AnimationFrame(new Rectangle(i * 64, 64 * 0, 64, 64)));
            }
            
            //LOOKING LEFT
            for (int i = 0; i < 2; i++)
            {
                idleAnimate.AddFrame(Direction.Left, new AnimationFrame(new Rectangle(i * 64, 64*1, 64, 64)));
            }

            //LOOKING DOWN (front)
            for (int i = 0; i < 2; i++)
            {
                idleAnimate.AddFrame(Direction.Down, new AnimationFrame(new Rectangle(i * 64, 64*2, 64, 64)));
            }

            //LOOKING RIGHT
            for (int i = 0; i < 2; i++)
            {
                idleAnimate.AddFrame(Direction.Right, new AnimationFrame(new Rectangle(i * 64, 64*3, 64, 64)));
            }
        }
        private void SetupWalkAnimations()  //load frames from the walk texture (tashfin)
        {
            // walking animations are 8 sprites not counting first static sprite
            // TO BE LOOPED

            //WALKING DOWN (front)
            for (int i = 0; i < 8; i++)
            {
                walkAnimate.AddFrame(Direction.Down, new AnimationFrame(new Rectangle(i * 64, 64*10, 64, 64)));
            }

            //WALKING UP (back)
            for (int i = 0; i < 8; i++)
            {
                walkAnimate.AddFrame(Direction.Up, new AnimationFrame(new Rectangle(i * 64, 64*8, 64, 64)));
            }

            //WALKING LEFT
            for (int i = 0; i < 8; i++)
            {
                walkAnimate.AddFrame(Direction.Left, new AnimationFrame(new Rectangle(i * 64, 64*9, 64, 64)));
            }

            //WALKING RIGHT
            for (int i = 0; i < 8; i++)
            {
                walkAnimate.AddFrame(Direction.Right, new AnimationFrame(new Rectangle(i * 64, 64*11, 64, 64)));
            }

        }


        public virtual void Update(GameTime gameTime)  // Required by IGameObject interface
        {
            // Can be overridden in inherited classes (hero.cs class bvb)
        }

        // Adds direction-based movement for derived classes
        public virtual void Move(Vector2 direction, GameTime gameTime)
        {
            isMoving = direction != Vector2.Zero;
            position += direction * 2; // *2 -> twice as fast

            // determine direction
            if (direction.X > 0) currentDirection = Direction.Right;
            else if (direction.X < 0) currentDirection = Direction.Left;
            else if (direction.Y > 0) currentDirection = Direction.Down;
            else if (direction.Y < 0) currentDirection = Direction.Up;

            if (isMoving)
            {
                walkAnimate.Update(gameTime, currentDirection); // Pass gameTime to Animate.Update             
            }
            else
            {
                idleAnimate.Update(gameTime, currentDirection);
            }

        }

        public virtual void Draw(SpriteBatch spriteBatch) //renders correct animation frame (idle when standing, walking when moving)
        {
            // Choose the texture based on the character’s state
            Texture2D currentTexture = isMoving ? walkTexture : idleTexture;
            var currentAnimate = isMoving ? walkAnimate : idleAnimate;
            //spriteBatch.Draw(currentTexture, position, animate.CurrentFrame.SourceRectangle, Color.White);
            // If idle, ensure the idle animation frames are used
            //if (!isMoving)
            //{
            //    // Set the animation to the idle state
            //    animate.Update(gameTime, currentDirection);
            //}
            
            // Draw the character using the appropriate texture (walking or idle)
            spriteBatch.Draw(currentTexture, position, currentAnimate.CurrentFrame.SourceRectangle, Color.White);
        }
    }
}
