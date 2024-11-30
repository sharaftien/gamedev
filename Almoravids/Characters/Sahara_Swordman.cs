using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Almoravids.Characters
{
    public class Sahara_Swordman : Enemy
    {
        public Sahara_Swordman(Texture2D idleTexture, Texture2D walkTexture, Vector2 startPosition, Character target)
            : base(idleTexture, walkTexture, startPosition, target)
        {
        }

        // You can override Update or Add Special Behaviors Here in the Future
    }
}
