using Almoravids.Characters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Almoravids.Animation
{
    public class Animate
    {
        public AnimationFrame CurrentFrame { get; set; }
        private Dictionary<Direction, List<AnimationFrame>> directionFrames;
        //private List<AnimationFrame> idleFrames;
        private int counter;
        private double frameMovement = 0;
        private double frameSpeed; //to be declared induvdiually

        public Animate(double speed = 0.1)
        {
            directionFrames = new Dictionary<Direction, List<AnimationFrame>>();
            frameSpeed = speed;
        }

        public void SetFrameSpeed(double speed)
        {
            frameSpeed = speed; //allows speed to be changed later
        }

        public void AddFrame(Direction direction, AnimationFrame animationFrame)
        {
            if (!directionFrames.ContainsKey(direction))
            {
                directionFrames[direction] = new List<AnimationFrame>();
            }

            directionFrames[direction].Add(animationFrame);

            //default DOWN (front) direction when first adding frames
            if (CurrentFrame == null)
            {
                CurrentFrame = animationFrame;
            }
        }

        public void Update(GameTime gameTime, Direction currentDirection)
        {
            //Get the list of frames for the current direction
            var frames = directionFrames[currentDirection];
            CurrentFrame = frames[counter];

            frameMovement += gameTime.ElapsedGameTime.TotalSeconds;
            if (frameMovement >= frameSpeed)
            {
                counter++;
                frameMovement = 0;
            }
            if (counter>=frames.Count)
            {
                counter = 0;
            }
            //frameMovement += CurrentFrame.SourceRectangle.Width * gameTime.ElapsedGameTime.TotalSeconds;
            //if (frameMovement >= CurrentFrame.SourceRectangle.Width/10)
            //{
            //    counter++;
            //    frameMovement = 0;
            //}
                
            //if (counter >= frames.Count)
            //    counter = 0;
        }
       
    }
}
