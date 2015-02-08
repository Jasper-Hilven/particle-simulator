using System.Collections;
using System.Collections.Generic;
using System.Linq;
using ParticleSimulation.Sorting;
using ParticleSimulation.Structuring;
using ParticleSimulation.Structuring.Sectioning;

namespace ParticleSimulation.Tactics.SideSort
{
    public class ParticleSection : GriddedParticleSection
    {
        //Xup //Xdown  ... CONTAIN COPIES!!
        public List<List<ReffedParticle>> sendParticles = new List<List<ReffedParticle>>();
        public List<Particle> BulkParticles;
        public int[] elementsPerSide;
        public ParticleSection(SectionGrid grid, ICore core, int elementsPerSide)
            : base(grid, core)
        {
            BulkParticles = new List<Particle>();
            this.elementsPerSide = new[] { elementsPerSide, elementsPerSide, elementsPerSide, elementsPerSide, elementsPerSide, elementsPerSide};
        }

        public override IEnumerable<Particle> Particles
        {
            get
            {
                return BulkParticles;
            }
        }

        public override void AddParticle(Particle particle)
        {
            BulkParticles.Add(particle);
        }

        public override void ApplyToAllParticles(ApplyToParticleDelegate applyTo)
        {
            for (int i = 0; i < BulkParticles.Count; i++)
            {
                BulkParticles[i] = applyTo(BulkParticles[i]);
            }

        }

        public struct ReffedParticle
        {
            public static ReffedParticle Null { get { return new ReffedParticle(){ particle = Particle.Null,reference = -1}; }}
            public Particle particle;
            public int reference;


        }
    }
}