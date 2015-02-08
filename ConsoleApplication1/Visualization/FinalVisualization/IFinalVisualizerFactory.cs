using ParticleSimulation.CostCalculation.CostReview;
using ParticleSimulation.CostCalculation.CostStorage;

namespace ParticleSimulation.Visualization.FinalVisualization
{
    public interface IFinalVisualizerFactory
    {
        IFinalVisualizer GetFinalVisualizer(ICostReviewer storage);
    }
}