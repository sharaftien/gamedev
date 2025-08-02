using Almoravids.Animation;
using Almoravids.Interfaces;

namespace Almoravids.Characters
{
    public class Tether : Character, IControllable
    {
        private Vector2 _inputDirection;

        public Tether(Texture2D texture, Vector2 startPosition)
            : base(texture, startPosition, "tether", 100f)
        {
            AnimationComponent = new AnimationComponent(new TetherCommonAnimation(texture), "tether");
        }

        public override void Update(GameTime gameTime)
        {
            MovementComponent.SetDirection(_inputDirection);
            MovementComponent.Update(gameTime);

            bool isMoving = MovementComponent.Velocity.LengthSquared() > 0.1f;
            AnimationComponent.Update(gameTime, _inputDirection, isMoving || true);
        }

        public void SetDirection(Vector2 direction)
        {
            _inputDirection = direction;
        }
    }

    public class TetherCommonAnimation : CommonAnimation
    {
        public TetherCommonAnimation(Texture2D texture)
            : base(texture, "lamtuni")
        {
            DefineAnimations();
        }

        public override void DefineAnimations()
        {
            DefineCommonAnimations();
        }
    }
}