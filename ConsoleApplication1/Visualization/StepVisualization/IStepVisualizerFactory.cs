using ParticleSimulation.CostCalculation.CostReview;
using ParticleSimulation.CostCalculation.CostStorage;

namespace ParticleSimulation.Visualization.StepVisualization
{
    public interface IStepVisualizerFactory
    {
        IStepVisualizer GetStepVisualizer(ICostReviewer storage);
    }
}