using ParticleSimulation.Structuring;

namespace ParticleSimulation.Visualization.StepVisualization
{
    public interface IStepVisualizer
    {
        void VisualizeStep(SimulationStructure structure, int step);
    }
}