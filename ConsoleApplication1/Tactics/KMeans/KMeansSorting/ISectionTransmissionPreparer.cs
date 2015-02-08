using ParticleSimulation.Structuring;

namespace ParticleSimulation.Tactics.KMeans.KMeansSorting
{
    public interface ISectionTransmissionPreparer
    {
        void PutParticlesForTransmission(SimulationStructure simulationStructure);
    }
}