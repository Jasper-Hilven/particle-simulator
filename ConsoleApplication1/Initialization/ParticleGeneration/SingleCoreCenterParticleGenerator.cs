using System;
using System.Collections.Generic;
using System.Linq;
using ParticleSimulation.Structuring;
using ParticleSimulation.Structuring.Sectioning;
using SharpDX;

namespace ParticleSimulation.Initialization.ParticleGeneration
{
    public class SingleCoreCenterParticleGenerator : IParticleGenerator
    {

        private Random random;

        public SingleCoreCenterParticleGenerator(Random random)
        {
            this.random = random;
        }

        public void GenerateParticles(CoreGrid grid, int particlesPerSection)
        {
            var sections = grid.GetCores().SelectMany(core => core.Sections);
            var sectionPositions = sections.Select(section => (section.UpperBound + section.LowerBound) / 2);
            var centerVector = new Vector3(sectionPositions.Average(section => section.X), sectionPositions.Average(section => section.Y), sectionPositions.Average(section => section.Z));
            var minVector = new Vector3(sectionPositions.Min(s => s.X), sectionPositions.Min(s => s.Y), sectionPositions.Min(s => s.Z));
            var maxVector = new Vector3(sectionPositions.Max(s => s.X), sectionPositions.Max(s => s.Y), sectionPositions.Max(s => s.Z));
            var minDistFromCenterSquared = sectionPositions.Select(position => (position - centerVector).LengthSquared()).Min();
            var centerSection = sections.First(s => (((s.UpperBound + s.LowerBound)/2 - centerVector).LengthSquared().Equals(minDistFromCenterSquared)));

            foreach (var section in sections)
            {
                for (var i = 0; i < particlesPerSection; i++)
                {
                    var delta = new Vector3(
                                            ((float)((maxVector.X - minVector.X) * (random.NextDouble() - 0.5f))),
                                            ((float)((maxVector.Y - minVector.Y) * (random.NextDouble() - 0.5f))),
                                            ((float)((maxVector.Z - minVector.Z) * (random.NextDouble() - 0.5f))));
                    centerSection.AddParticle(new Particle() { Position = centerVector + delta / 10, Velocity = delta });
                }
            }
        }
    }
}