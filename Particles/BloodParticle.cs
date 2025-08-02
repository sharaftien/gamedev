
namespace Almoravids.Particles
{
    public class BloodParticle : Particle
    {
        public BloodParticle(Vector2 position, Vector2 direction)
            : base(position, direction * 50f, 0.4f, Color.Red) { }
    }
}

