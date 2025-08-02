
namespace Almoravids.Interfaces
{
    public interface IGameState
    {
        void Initialize(ContentManager content, GraphicsDevice graphicsDevice);
        void Update(GameTime gameTime);
        void Draw(SpriteBatch spriteBatch);

        IGameState GetNextState();
    }
}