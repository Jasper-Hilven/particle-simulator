using System;
using System.Drawing;
using ParticleSimulation.Structuring;

namespace ParticleSimulation.Visualization.StepVisualization.PictureCombining
{
    public interface IPictureCoordinateConverter
    {
        Tuple<int, int> GetPictureCoordinatesFor(float x, float z, SimulationStructure structure);
        void SetParticleWindowSize(Point size);
    }
}