using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Almoravids.Collision
{
    public class CollisionComponent
    {
        private Rectangle _boundingBox;
        private readonly float _width;
        private readonly float _height;

        public CollisionComponent(float width, float height)
        {
            _width = width;
            _height = height;
            _boundingBox = new Rectangle(0, 0, (int)width, (int)height);
        }

        public void Update(Vector2 position)
        {
            _boundingBox.X = (int)position.X;
            _boundingBox.Y = (int)position.Y;
        }

        public Rectangle BoundingBox => _boundingBox; // for checking collisions later
    }
}
