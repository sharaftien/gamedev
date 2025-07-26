using MonoGame.Extended.Tiled;

namespace Almoravids.Collision
{
    public class CollisionComponent
    {
        private Rectangle _boundingBox;
        private readonly float _width;
        private readonly float _height;
        private readonly float _offsetX; // offset whitespace
        private readonly float _offsetY; // offset whitespace

        public CollisionComponent(float width, float height, float offsetX = 0f, float offsetY = 0f)
        {
            _width = width;
            _height = height;
            _offsetX = offsetX; //whitespace fix
            _offsetY = offsetY;
            _boundingBox = new Rectangle(0, 0, (int)width, (int)height);
        }

        public void Update(Vector2 position)
        {
            _boundingBox.X = (int)(position.X + _offsetX); // apply offset
            _boundingBox.Y = (int)(position.Y + _offsetY); 
        }

        public bool CheckMapCollision(TiledMapObjectLayer collisionLayer, out Vector2 resolution)
        {
            resolution = Vector2.Zero;
            foreach (var obj in collisionLayer.Objects)
            {
                Rectangle objRect = new Rectangle((int)obj.Position.X, (int)obj.Position.Y, (int)obj.Size.Width, (int)obj.Size.Height);
                if (_boundingBox.Intersects(objRect))
                {
                    resolution = CalculateResolution(objRect);
                    return true;
                }
            }
            return false;
        }

        private Vector2 CalculateResolution(Rectangle otherRect)
        {
            Vector2 centerA = new Vector2(_boundingBox.Center.X, _boundingBox.Center.Y);
            Vector2 centerB = new Vector2(otherRect.Center.X, otherRect.Center.Y);
            Vector2 direction = centerA - centerB;
            float overlapX = (_boundingBox.Width + otherRect.Width) / 2 - Math.Abs(centerA.X - centerB.X);
            float overlapY = (_boundingBox.Height + otherRect.Height) / 2 - Math.Abs(centerA.Y - centerB.Y);

            if (overlapX < overlapY)
            {
                return new Vector2(direction.X > 0 ? overlapX : -overlapX, 0); // push out on x-axis
            }
            else
            {
                return new Vector2(0, direction.Y > 0 ? overlapY : -overlapY); // push out on y-axis
            }
        }

        public Rectangle BoundingBox => _boundingBox; // for checking collisions later
    }
}