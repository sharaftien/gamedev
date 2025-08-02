
namespace Almoravids.UI
{
    public class ButtonRenderer
    {
        public string Label { get; }
        public Color DotColor { get; }
        public Vector2 Offset { get; }
        public Rectangle Bounds { get; private set; }
        private readonly SpriteFont _font;
        private readonly GraphicsDevice _graphicsDevice;
        private readonly Action _onClick;

        public ButtonRenderer(SpriteFont font, string label, Color color, Vector2 offset, GraphicsDevice graphicsDevice, Action onClick)
        {
            _font = font;
            Label = label;
            DotColor = color;
            Offset = offset;
            _graphicsDevice = graphicsDevice;
            _onClick = onClick;
            CalculateBounds(); // initialize bounds
        }

        private void CalculateBounds()
        {
            Vector2 textSize = _font.MeasureString(Label);
            int width = 16 + 16 + (int)textSize.X; // dot + spacing + text width
            int height = Math.Max(16, (int)textSize.Y);
            int centerX = _graphicsDevice.Viewport.Width / 2;
            int centerY = _graphicsDevice.Viewport.Height / 2;
            Bounds = new Rectangle((int)(centerX + Offset.X - width / 2), (int)(centerY + Offset.Y - height / 2), width, height);
        }

        public void Update(MouseState mouse)
        {
            if (mouse.LeftButton == ButtonState.Pressed && Bounds.Contains(mouse.Position))
            {
                _onClick?.Invoke();
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            // draw dot
            var dotRect = new Rectangle(Bounds.X, Bounds.Y, 16, 16);
            spriteBatch.Draw(Game1.Pixel, dotRect, DotColor);

            // 24px to the right of the square
            var labelPos = new Vector2(Bounds.X +24, Bounds.Y + 4);

            spriteBatch.DrawString(_font, Label, labelPos, Color.White);
        }
    }
}
