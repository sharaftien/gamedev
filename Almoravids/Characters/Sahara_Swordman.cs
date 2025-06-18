using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Almoravids.Characters
{
    public class Sahara_Swordman : Enemy
    {
        public Sahara_Swordman(Texture2D walkTexture, Texture2D idleTexture, Vector2 startPosition, Character target, string characterType = "swordman")
            : base(walkTexture, idleTexture, startPosition, target, characterType)
        {
        }

        // You can override Update or Add Special Behaviors Here in the Future
        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
        }
    }
}