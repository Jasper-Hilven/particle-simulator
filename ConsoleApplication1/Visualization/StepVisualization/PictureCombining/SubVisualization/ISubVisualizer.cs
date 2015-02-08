using Emgu.CV;
using Emgu.CV.Structure;
using ParticleSimulation.Structuring;

namespace ParticleSimulation.Visualization.StepVisualization.PictureCombining.SubVisualization
{
    public interface ISubVisualizer
    {
        void DrawVisualization(SimulationStructure structure, Image<Rgb, byte> image);
    }
}