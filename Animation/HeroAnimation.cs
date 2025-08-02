
namespace Almoravids.Animation
{
    public class HeroAnimation : CommonAnimation
    {
        public HeroAnimation(Texture2D texture) : base(texture, "hero")
        {
        }

        public override void DefineAnimations()
        {
            // idle animation
            string[] directions = { "up", "left", "down", "right" };
            int[] idleStartIndices = { 61, 74, 87, 100 };
            for (int i = 0; i < directions.Length; i++)
            {
                string direction = directions[i];
                spriteSheet.DefineAnimation($"idle_{direction}", builder =>
                {
                    builder.IsLooping(true);
                    builder.AddFrame(idleStartIndices[i], TimeSpan.FromSeconds(0.2f));
                    builder.AddFrame(idleStartIndices[i] + 1, TimeSpan.FromSeconds(0.2f));
                });
            }

            // walk and death animations
            DefineCommonAnimations();
        }
    }
}