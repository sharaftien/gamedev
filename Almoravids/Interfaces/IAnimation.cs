using MonoGame.Extended.Graphics;

namespace Almoravids.Interfaces
{
    public interface IAnimation
    {
        SpriteSheet GetSpriteSheet();
        void DefineAnimations();
    }
}