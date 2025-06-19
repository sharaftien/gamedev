using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Almoravids.Characters
{
    public class Sahara_Swordsman : Enemy
    {
        public Sahara_Swordsman(Texture2D texture, Vector2 startPosition, Character target, string characterType = "swordman", float speed = 80f)
            : base(texture, startPosition, target, characterType, speed)
        {
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
        }
    }
}