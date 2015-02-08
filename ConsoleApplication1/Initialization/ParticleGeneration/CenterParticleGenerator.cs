using System;
using ParticleSimulation.Structuring;
using System.Linq;
using SharpDX;

namespace ParticleSimulation.Initialization.ParticleGeneration
{
    /// <summary>
    /// Generate particles that are only in the center of the simulation space, but distributed smoothly over the whole core space.
    /// </summary>
    public class CenterParticleGenerator : IParticleGenerator
    {
        private Random random;

        public CenterParticleGenerator(Random random)
        {
            this.random = random;
        }

        public void GenerateParticles(CoreGrid grid, int particlesPerSection)
        {
            var sectionPositions = grid.GetCores().SelectMany(core => core.Sections).Select(section => (section.UpperBound + section.LowerBound) / 2);
            var centerVector = new Vector3(sectionPositions.Average(section => section.X), sectionPositions.Average(section => section.Y), sectionPositions.Average(section => section.Z));
            var minVector = new Vector3(sectionPositions.Min(s => s.X), sectionPositions.Min(s => s.Y), sectionPositions.Min(s => s.Z));
            var maxVector = new Vector3(sectionPositions.Max(s => s.X), sectionPositions.Max(s => s.Y), sectionPositions.Max(s => s.Z));

            foreach (var section in grid.GetCores().SelectMany(c => c.Sections))
            {
                for (var i = 0; i < particlesPerSection; i++)
                {
                    var delta = new Vector3(
                                            ((float)((maxVector.X - minVector.X) * (random.NextDouble() - 0.5f))),
                                            ((float)((maxVector.Y - minVector.Y) * (random.NextDouble() - 0.5f))),
                                            ((float)((maxVector.Z - minVector.Z) * (random.NextDouble() - 0.5f))));
                    section.AddParticle(new Particle() { Position = centerVector + delta / 10, Velocity = delta });
                }
            }
        }
    }
}