using System.Collections.Generic;
using System.Linq;
using ParticleSimulation.Structuring;
using ParticleSimulation.Structuring.Sectioning;
using SharpDX;

namespace ParticleSimulation.Sorting
{
    public class NeighbourTransferSection : GriddedParticleSection
    {
        public NeighbourTransferSection(SectionGrid grid, ICore core) : base(grid, core)
        {
            CurrentParticles = new List<Particle>();
            for (var i = 0; i < 6; i++)
            {
                ToTransmit[i] = new List<Particle>();
            }    
        }

        public Vector3 CurrentPosition { get; set; }
        public bool InitializedPosition = false; 
        public Vector3[] NeighbourPositions = new Vector3[6];
        public List<Particle>[] ToTransmit = new List<Particle>[6];
        public float LocationInertia { get; set; }
        public float LocationStifness { get; set; }
        public float MaximumDifferenceRadiusSquared { get; set; }

        public List<Particle> CurrentParticles { get; set; }

        public override IEnumerable<Particle> Particles
        {
            get
            {
                foreach (var particle in CurrentParticles)
                {
                    yield return particle;
                }
                foreach (var particle1 in ToTransmit.SelectMany(particle => particle))
                {
                    yield return particle1;
                }
            }
        }

        public override void AddParticle(Particle particle)
        {
            CurrentParticles.Add(particle);
        }

        public override void ApplyToAllParticles(ApplyToParticleDelegate applyTo)
        {
            for (var i = 0; i < CurrentParticles.Count; i++)
            {
                CurrentParticles[i] = applyTo(CurrentParticles[i]);
            }
            for (var i = 0; i < ToTransmit.Length; i++)
            {
                for (var j = 0; j < ToTransmit[i].Count; j++)
                {
                    ToTransmit[i][j] = applyTo(ToTransmit[i][j]);

                }
            }
        }
    }
}