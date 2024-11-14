using Almoravids.Animation;
using Almoravids.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Almoravids.Characters
{
    public abstract class Character : IGameObject
    {
        protected Texture2D idleTexture;
        protected Texture2D walkTexture;
        protected Animate animate;
        protected Vector2 position;

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
            // walking animation is 8 frames (first frame is standing still so doesn't count)
            for (int i = 0; i < 8; i++)
            {
                animate.AddFrame(new AnimationFrame(new Rectangle(i * 64, 9*64, 64, 64)));
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
            position += direction * 2; // *4 -> 4 times as fast

            // Update animation frame if moving
            if (isMoving)
            {
                animate.Update(gameTime); // Pass gameTime to Animate.Update
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
