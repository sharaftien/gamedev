
namespace Almoravids.Collision
{
    public class ArrowCollision
    {
        private readonly float _width;
        private readonly float _height;
        private readonly float _offsetX;
        private readonly float _offsetY;
        private readonly Func<Vector2> _positionGetter;
        private readonly Func<float> _rotationGetter;

        public ArrowCollision(float width, float height, float offsetX, float offsetY, Func<Vector2> positionGetter, Func<float> rotationGetter)
        {
            _width = width;
            _height = height;
            _offsetX = offsetX;
            _offsetY = offsetY;
            _positionGetter = positionGetter;
            _rotationGetter = rotationGetter;
        }

        public Rectangle BoundingBox
        {
            get
            {
                var rotation = _rotationGetter();
                var position = _positionGetter();
                Vector2 center = position + new Vector2(_offsetX + _width / 2f, _offsetY + _height / 2f);

                Vector2 right = new((float)Math.Cos(rotation), (float)Math.Sin(rotation));
                Vector2 up = new(-right.Y, right.X);

                Vector2 halfW = right * (_width / 2f);
                Vector2 halfH = up * (_height / 2f);

                Vector2[] corners = new Vector2[4];
                corners[0] = center - halfW - halfH;
                corners[1] = center + halfW - halfH;
                corners[2] = center + halfW + halfH;
                corners[3] = center - halfW + halfH;

                float minX = corners.Min(c => c.X);
                float minY = corners.Min(c => c.Y);
                float maxX = corners.Max(c => c.X);
                float maxY = corners.Max(c => c.Y);

                return new Rectangle((int)minX, (int)minY, (int)(maxX - minX), (int)(maxY - minY));
            }
        }
    }
}
