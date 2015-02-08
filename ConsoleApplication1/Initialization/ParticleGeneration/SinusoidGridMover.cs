using System;
using ParticleSimulation.Moving;
using ParticleSimulation.Structuring;
using ParticleSimulation.Structuring.Sectioning;

namespace ParticleSimulation.Initialization.ParticleGeneration
{
    class SinusoidGridMover:IParticleMover
    {
        private IParticleSection overParticleSection;
        public Array[,,] Particles { get; set; } 

        public void MoveParticles(SimulationStructure structure, float step, int simStep)
        {
            for (int i = 0; i < Particles.GetLength(0); i++)
            {
                for (int j = 0; j < Particles.GetLength(1); j++)
                {
                    for (int k = 0; k < Particles.GetLength(2); k++)
                    {
                        throw new NotImplementedException();
                    }
                }
            }
        }

        public void SetBoundarySection(IParticleSection ovrParticleSection)
        {
            overParticleSection = ovrParticleSection;
        }
    }
}
