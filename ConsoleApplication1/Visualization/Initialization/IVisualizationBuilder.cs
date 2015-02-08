using ParticleSimulation.Visualization.FinalVisualization;
using ParticleSimulation.Visualization.StepVisualization;

namespace ParticleSimulation.Visualization.Initialization
{
    public interface IVisualizationBuilder:IFinalVisualizerFactory,IStepVisualizerFactory
    {
        
    }
}