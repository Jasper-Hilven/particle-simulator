using System.Collections.Generic;
using SharpDX;

namespace ParticleSimulation.Structuring.Sectioning
{
    public interface IParticleSection : ISection
    {
        IEnumerable<Particle> Particles { get; }
        ICore Core { get; }
        IParticleSection GetContainingParticle(Vector3 pos);
        void AddParticle(Particle particle);
        void ApplyToAllParticles(ApplyToParticleDelegate applyTo);
    }

    public delegate Particle ApplyToParticleDelegate(Particle particle);
}