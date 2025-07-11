
namespace Almoravids.UI
{
    public class TextRenderer
    {
        private readonly SpriteFont _font;
        private readonly string _text;
        private readonly Color _color;
        private readonly float _scale;
        private readonly bool _centerHorizontaally;
        private readonly bool _centerVertically;
        private readonly GraphicsDevice _graphicsDevice;

        public TextRenderer(SpriteFont font, string text, Color color, float scale, bool centerHorizontally, bool centerVertically, GraphicsDevice graphicsDevice)
        {
            _font = font;
            _text = text;
            _color = color;
            _scale = scale;
            _centerHorizontaally = centerHorizontally;
            _centerVertically = centerVertically;
            _graphicsDevice = graphicsDevice;
        }

        public void Draw(SpriteBatch spriteBatch, float xOffset = 0f, float yOffset = 0f)
        {
            Vector2 textSize = _font.MeasureString(_text) * _scale;
            Vector2 drawPosition = Vector2.Zero;

            if (_centerHorizontaally)
            {
                drawPosition.X = (_graphicsDevice.Viewport.Width - textSize.X) / 2 + xOffset;
            }
            else
            {
                drawPosition.X = xOffset;
            }

            if (_centerVertically)
            {
                drawPosition.Y = (_graphicsDevice.Viewport.Height - textSize.Y) / 2 + yOffset;
            }
            else
            {
                drawPosition.Y = yOffset;
            }

            spriteBatch.DrawString(_font, _text, drawPosition, _color, 0f, Vector2.Zero, _scale, SpriteEffects.None, 0f);
        }
    }
}