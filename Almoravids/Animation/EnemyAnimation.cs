
namespace Almoravids.Animation
{
    public class EnemyAnimation : CommonAnimation
    {
        public EnemyAnimation(Texture2D texture, string characterType) : base(texture, characterType)
        {
        }

        public override void DefineAnimations()
        {
            DefineCommonAnimations();
        }
    }
}