using ParticleSimulation.Structuring;

namespace ParticleSimulation.Tactics.KMeans.KMeansSectioning
{
    public interface ISectionPositionUpdater
    {
        void UpdateSectionPositions(SimulationStructure simulationStructure);
    }
}