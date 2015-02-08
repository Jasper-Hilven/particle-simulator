using ParticleSimulation.Structuring;

namespace ParticleSimulation.Sorting
{
    public interface IParticleTransmitter
    {
        void TransmitParticles(SimulationStructure structure, int step);
    }
}