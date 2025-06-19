using Almoravids.Animation;
using Almoravids.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Almoravids.Movement;

namespace Almoravids.Characters
{
    public enum Direction
    {
        Up, Down, Left, Right
    }

    public abstract class Character : IGameObject
    {
        public AnimationComponent AnimationComponent { get; set; }
        public MovementComponent MovementComponent { get; set; }

        public Vector2 Position => MovementComponent.Position;

        public Character(Texture2D texture, Vector2 startPosition, string characterType, float speed)
        {
            AnimationComponent = new AnimationComponent(texture, characterType);
            MovementComponent = new MovementComponent(startPosition, speed);
        }

        public virtual void Update(GameTime gameTime)
        {
            MovementComponent.Update(gameTime);
            AnimationComponent.Update(gameTime, MovementComponent.Velocity);
        }

        public virtual void Draw(SpriteBatch spriteBatch)
        {
            AnimationComponent.Draw(spriteBatch, MovementComponent.Position);
        }
    }
}