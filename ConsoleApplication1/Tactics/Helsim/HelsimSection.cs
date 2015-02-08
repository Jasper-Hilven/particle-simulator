using System.Collections.Generic;
using ParticleSimulation.Structuring;
using ParticleSimulation.Structuring.Sectioning;

namespace ParticleSimulation.Tactics.Helsim
{

    public class HelsimSection : GriddedParticleSection
    {
        private readonly List<Particle> particles  = new List<Particle>(); 
        public HelsimSection(SectionGrid grid, ICore core) : base(grid, core)
        {
        }

        public override IEnumerable<Particle> Particles
        {
            get { return particles; }
        }

        public override void AddParticle(Particle particle)
        {
            particles.Add(particle);
        }

        public override void ApplyToAllParticles(ApplyToParticleDelegate applyTo)
        {
            for (var i = 0; i < particles.Count; i++)
            {
                particles[i] = applyTo(particles[i]);
            }
        }
    }
}