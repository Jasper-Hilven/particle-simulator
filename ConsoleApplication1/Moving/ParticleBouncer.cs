using ParticleSimulation.Structuring;
using ParticleSimulation.Structuring.Sectioning;
using SharpDX;

namespace ParticleSimulation.Moving
{
    public class ParticleBouncer
    {
        private IParticleSection overParticleSection;
        
        public void SetBoundarySection(IParticleSection overParticleSection)
        {
            this.overParticleSection = overParticleSection;
        }
        public void BounceParticle(ref Particle particle)
        {
            if (particle.Position.X < overParticleSection.LowerBound.X)
            {
                particle.Position = new Vector3(2 * overParticleSection.LowerBound.X - particle.Position.X, particle.Position.Y, particle.Position.Z);
                particle.Velocity = new Vector3(-particle.Velocity.X, particle.Velocity.Y, particle.Velocity.Z);
            }
            if (particle.Position.X >= overParticleSection.UpperBound.X)
            {
                particle.Position = new Vector3(2 * overParticleSection.UpperBound.X - particle.Position.X - 0.00001f, particle.Position.Y, particle.Position.Z);
                particle.Velocity = new Vector3(-particle.Velocity.X, particle.Velocity.Y, particle.Velocity.Z);
            }
            if (particle.Position.Y < overParticleSection.LowerBound.Y)
            {
                particle.Position = new Vector3(particle.Position.X, 2 * overParticleSection.LowerBound.Y - particle.Position.Y, particle.Position.Z);
                particle.Velocity = new Vector3(particle.Velocity.X, -particle.Velocity.Y, particle.Velocity.Z);
            }
            if (particle.Position.Y >= overParticleSection.UpperBound.Y)
            {
                particle.Position = new Vector3(particle.Position.X, 2 * overParticleSection.UpperBound.Y - particle.Position.Y - 0.00001f, particle.Position.Z);
                particle.Velocity = new Vector3(particle.Velocity.X, -particle.Velocity.Y, particle.Velocity.Z);
            }
            if (particle.Position.Z < overParticleSection.LowerBound.Z)
            {
                particle.Position = new Vector3(particle.Position.X, particle.Position.Y, 2 * overParticleSection.LowerBound.Z - particle.Position.Z);
                particle.Velocity = new Vector3(particle.Velocity.X, particle.Velocity.Y, -particle.Velocity.Z);
            }
            if (particle.Position.Z >= overParticleSection.UpperBound.Z)
            {
                particle.Position = new Vector3(particle.Position.X, particle.Position.Y, 2 * overParticleSection.UpperBound.Z - particle.Position.Z - 0.00001f);
                particle.Velocity = new Vector3(particle.Velocity.X, particle.Velocity.Y, -particle.Velocity.Z);
            }
        }
    }
}