using ParticleSimulation.Structuring;
using ParticleSimulation.Structuring.Sectioning;

namespace ParticleSimulation.Moving
{
    public interface IParticleMover
    {
        void MoveParticles(SimulationStructure structure, float step, int simStep);
        void SetBoundarySection(IParticleSection overParticleSection);
    }
}