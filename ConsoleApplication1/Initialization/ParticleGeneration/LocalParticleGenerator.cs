using System;
using ParticleSimulation.Structuring;
using ParticleSimulation.Structuring.Sectioning;
using SharpDX;

namespace ParticleSimulation.Initialization.ParticleGeneration
{
    /// <summary>
    /// Each section has only particles within section.
    /// </summary>
    public class LocalParticleGenerator : IParticleGenerator
    {
        private readonly Random random;

        public LocalParticleGenerator(Random random)
        {
            this.random = random;
        }

        public void GenerateParticles(CoreGrid grid, int particlesPerSection)
        {
            GenerateLocalParticles(grid, particlesPerSection);
        }

        private void GenerateLocalParticles(CoreGrid cGrid, int particlesPerSection)
        {
            foreach (var core in cGrid.GetCores())
            {
                foreach (var section in core.Sections)
                {
                    for (var i = 0; i < particlesPerSection; i++)
                    {
                        var position = section.LowerBound +
                          new Vector3(
                                      (section.UpperBound.X - section.LowerBound.X) *
                                      (float)Math.Abs(random.NextDouble()),
                                      (section.UpperBound.Y - section.LowerBound.Y) *
                                      (float)Math.Abs(random.NextDouble()),
                                      (section.UpperBound.Z - section.LowerBound.Z) *
                                      (float)Math.Abs(random.NextDouble()));
                        var velocity = new Vector3(1f - 2 * (float)random.NextDouble(),
                            1f - 2 * (float)random.NextDouble(),
                            1f - 2 * (float)random.NextDouble());

                        section.AddParticle(new Particle { Position = position, Velocity = velocity });
                    }
                }
            }
        }
    }
    public class SingleNodeLocalParticleGenerator : IParticleGenerator
    {
        private readonly Random random;

        public SingleNodeLocalParticleGenerator(Random random)
        {
            this.random = random;
        }

        public void GenerateParticles(CoreGrid grid, int particlesPerSection)
        {
            GenerateLocalParticles(grid, particlesPerSection);
        }

        private void GenerateLocalParticles(CoreGrid cGrid, int particlesPerSection)
        {
            bool didCore = false;
            foreach (var core in cGrid.GetCores())
            {
                if (didCore)
                    continue;
                foreach (var section in core.Sections)
                {
                    for (var i = 0; i < particlesPerSection; i++)
                    {
                        var position = section.LowerBound +
                          new Vector3(
                                      (section.UpperBound.X - section.LowerBound.X) *
                                      (float)Math.Abs(random.NextDouble()),
                                      (section.UpperBound.Y - section.LowerBound.Y) *
                                      (float)Math.Abs(random.NextDouble()),
                                      (section.UpperBound.Z - section.LowerBound.Z) *
                                      (float)Math.Abs(random.NextDouble()));
                        var velocity = new Vector3(1f - 2 * (float)random.NextDouble(),
                            1f - 2 * (float)random.NextDouble(),
                            1f - 2 * (float)random.NextDouble());

                        section.AddParticle(new Particle { Position = position, Velocity = velocity });
                    }
                }
                didCore = true;
            }
        }
    }


}