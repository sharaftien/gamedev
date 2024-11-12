using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Almoravids.Animation;
using Almoravids.Interfaces;
using System.Threading;

namespace Almoravids
{
    public class Hero : IGameObject
    {
        Texture2D heroTexture;
        Animate animate;

        public Hero(Texture2D texture)
        {
            heroTexture = texture;
            animate = new Animate();
            for (int i = 0; i < 8; i++)
            {
                animate.AddFrame(new AnimationFrame(new Rectangle((64 * i)+64, 64 * 11, 64, 64)));
            }

        }

        public void Update(GameTime gameTime)
        {
            animate.Update(gameTime);
        }
        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(heroTexture, new Vector2(10, 10),animate.CurrentFrame.SourceRectangle, Color.White);
        }
    }
}
