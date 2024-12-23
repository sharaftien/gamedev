﻿using Almoravids.Interfaces;
using Almoravids.Animation;
using Almoravids.Characters;
using Almoravids.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Almoravids.Characters
{
    public abstract class Enemy : Character, IGameObject
    {
        protected Character target; // The target (e.g., Tashfin)

        public Enemy(Texture2D idleTexture, Texture2D walkTexture, Vector2 startPosition, Character target)
            : base(idleTexture, walkTexture, startPosition)
        {
            this.target = target;
        }

        public override void Update(GameTime gameTime)
        {
            // Calculate the direction toward the target
            Vector2 direction = target.Position - this.Position;  // Use Position here

            // Normalize direction to ensure consistent movement
            if (direction.Length() > 0)
                direction.Normalize();

            // Move toward the target
            Move(direction, gameTime);
        }
    }
}

