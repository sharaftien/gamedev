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
        //private List<AnimationFrame> frames;
        private int counter;
        private double frameMovement = 0;

        public Animate()
        {
            directionFrames = new Dictionary<Direction, List<AnimationFrame>>();
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

            frameMovement += CurrentFrame.SourceRectangle.Width * gameTime.ElapsedGameTime.TotalSeconds;
            if (frameMovement >= CurrentFrame.SourceRectangle.Width/10)
            {
                counter++;
                frameMovement = 0;
            }
                
            if (counter >= frames.Count)
                counter = 0;
        }
       
    }
}
