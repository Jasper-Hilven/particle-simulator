using System;
using ParticleSimulation.Structuring;
using SharpDX;

namespace ParticleSimulation.Initialization.ParticleGeneration
{
    public class RandomParticleGenerator:IParticleGenerator
    {
        private Random random;

        public RandomParticleGenerator(Random random)
        {
            this.random = random;
        }
        private void GenerateRandomParticles(CoreGrid cGrid, int particlesPerCore)
        {
            foreach (var core in cGrid.GetCores())
            {
                foreach (var section in core.Sections)
                {


                    for (var i = 0; i < particlesPerCore; i++)
                    {
                        var position = new Vector3((float) random.NextDouble(), (float) random.NextDouble(),
                                                   (float) random.NextDouble());

                        var velocity = new Vector3(1f - 2*(float) random.NextDouble(),
                                                   1f - 2*(float) random.NextDouble(),
                                                   1f - 2*(float) random.NextDouble());

                        section.AddParticle(new Particle() {Position = position, Velocity = velocity});
                    }
                }
            }


        }


        public void GenerateParticles(CoreGrid grid, int particlesPerSection)
        {
            GenerateRandomParticles(grid,particlesPerSection);
        }
    }
}