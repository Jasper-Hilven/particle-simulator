using ParticleSimulation.Structuring;

namespace ParticleSimulation.Sorting
{
    
    public interface ISorter
    {
        void Sort(SimulationStructure simulationStructure, int step);
    }
}