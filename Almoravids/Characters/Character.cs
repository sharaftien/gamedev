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
        protected AnimationComponent AnimationComponent;
        protected Vector2 position;
        protected bool isMoving;

        public Vector2 Position
        {
            get { return position; }
            protected set { position = value; }
        }

        public Character(Texture2D walkTexture, Texture2D idleTexture, Vector2 startPosition, string characterType)
        {
            AnimationComponent = new AnimationComponent(walkTexture, idleTexture, characterType);
            position = startPosition;
        }

        public virtual void Update(GameTime gameTime)
        {
            // can be overridden in inherited classes 
        }

        public virtual void Move(Vector2 direction, GameTime gameTime)
        {
            isMoving = direction != Vector2.Zero;
            position += direction * 2; // *2 -> twice as fast

            // determine direction
            AnimationComponent.Update(gameTime, direction);
        }

        public virtual void Draw(SpriteBatch spriteBatch)
        {
            AnimationComponent.Draw(spriteBatch, position);
        }
    }
}