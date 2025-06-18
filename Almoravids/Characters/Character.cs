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
        protected Direction currentDirection;

        protected Vector2 position;

        public Vector2 Position
        {
            get { return position; }
            protected set { position = value; }
        }

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
            SetupAnimations();
        }

        private void SetupAnimations()
        {
            walkAnimate.LoadFrames(walkTexture, Direction.Down, 8, 10); // 8 frames, row 10
            walkAnimate.LoadFrames(walkTexture, Direction.Up, 8, 8);   // 8 frames, row 8
            walkAnimate.LoadFrames(walkTexture, Direction.Left, 8, 9); // 8 frames, row 9
            walkAnimate.LoadFrames(walkTexture, Direction.Right, 8, 11); // 8 frames, row 11

            idleAnimate.LoadFrames(idleTexture, Direction.Up, 2, 0);   // 2 frames, row 0
            idleAnimate.LoadFrames(idleTexture, Direction.Left, 2, 1); // 2 frames, row 1
            idleAnimate.LoadFrames(idleTexture, Direction.Down, 2, 2); // 2 frames, row 2
            idleAnimate.LoadFrames(idleTexture, Direction.Right, 2, 3); // 2 frames, row 3
        }

        public virtual void Update(GameTime gameTime)
        {
            // Can be overridden in inherited classes
        }

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
                walkAnimate.Update(gameTime, currentDirection);
            }
            else
            {
                idleAnimate.Update(gameTime, currentDirection);
            }
        }

        public virtual void Draw(SpriteBatch spriteBatch)
        {
            Texture2D currentTexture = isMoving ? walkTexture : idleTexture;
            var currentAnimate = isMoving ? walkAnimate : idleAnimate;
            spriteBatch.Draw(currentTexture, position, currentAnimate.CurrentFrame.SourceRectangle, Color.White);
        }
    }
}