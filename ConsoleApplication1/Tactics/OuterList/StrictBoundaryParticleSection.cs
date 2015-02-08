using System.Collections.Generic;
using ParticleSimulation.Structuring;
using ParticleSimulation.Structuring.Sectioning;

namespace ParticleSimulation.Tactics.OuterList
{
    public class StrictBoundaryParticleSection : GriddedParticleSection
    {
        //represents the particles that should be in the grid
        public List<Particle> CentralParticles { get; set; }
        //represent the particles that are on the border of the grid 0: lowX, 1 highX, 2 lowY, 3 highY...
        public readonly List<Particle>[] OuterParticleGroups;

        
        public void RemoveParticle(Particle particle)
        {
            for (var i = 0; i < 6; i++)
            {
                if (!OuterParticleGroups[i].Contains(particle)) continue;
                OuterParticleGroups[i].Remove(particle);
                return;
            }

        }


        public StrictBoundaryParticleSection(SectionGrid grid,ICore core):base(grid,core)
        {
            OuterParticleGroups = new List<Particle>[6];
            for (var i = 0; i < 6; i++)
            {
                OuterParticleGroups[i] = new List<Particle>();
            }
            CentralParticles = new List<Particle>();
        }

        public override IEnumerable<Particle> Particles
        {
            get
            {
                foreach (var centralParticle in CentralParticles)
                {
                    yield return centralParticle;
                }
                foreach (var outerParticleGroup in OuterParticleGroups)
                {
                    foreach (var particle in outerParticleGroup)
                    {
                        yield return particle;
                    }
                }

            }
        }
        
        public override void AddParticle(Particle particle)
        {
            CentralParticles.Add(particle);
        }

        public override void ApplyToAllParticles(ApplyToParticleDelegate applyTo)
        {
            for (var i = 0; i < CentralParticles.Count; i++)
            {
                CentralParticles[i] = applyTo(CentralParticles[i]);
            }
            for (var i = 0; i < OuterParticleGroups.Length; i++)
            {
                for (var j = 0; j < OuterParticleGroups[i].Count; j++)
                {
                    OuterParticleGroups[i][j] = applyTo(OuterParticleGroups[i][j]);
                }
            } 
            
        }
    }
}