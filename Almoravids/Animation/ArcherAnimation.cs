namespace Almoravids.Animation
{
    public class ArcherAnimation : CommonAnimation
    {
        public ArcherAnimation(Texture2D texture) : base(texture, "archer")
        {
        }

        public override void DefineAnimations()
        {
            DefineCommonAnimations();

            string[] directions = { "up", "left", "down", "right" };
            int[] shootStartIndices = { 210, 223, 236, 249 }; // 210-219, 223-232, 236-245, 249-258
            for (int i = 0; i < directions.Length; i++)
            {
                string direction = directions[i];
                int shootStart = shootStartIndices[i];

                spriteSheet.DefineAnimation($"shoot_{direction}", builder =>
                {
                    builder.IsLooping(true);
                    for (int j = 0; j < 10; j++)
                    {
                        builder.AddFrame(shootStart + j, TimeSpan.FromSeconds(0.5)); // half a second per frame
                    }
                });
            }
        }
    }
}