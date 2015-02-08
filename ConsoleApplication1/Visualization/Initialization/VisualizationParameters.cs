using System.Collections.Generic;
using System.Drawing;
using ParticleSimulation.Visualization.StepVisualization.PictureCombining;
using ParticleSimulation.Visualization.StepVisualization.PictureCombining.SubVisualization;

namespace ParticleSimulation.Visualization.Initialization
{
    public class VisualizationParameters
    {
        
        public string MainOutputPath { get; set; }
        public int SnapTime { get; set; }
        public List<ISubVisualizer> ParticleSubVisualizers { get; set; }
        public Point CostVisualizationSize { get; set; }
        //public ICostVisualizerFactory CostVisualizerFactory { get; set; }
        public string MergedPictureAddPath { get; set; }
        public string VideoAddPath { get; set; }
        public string VideoName { get; set; }
        private Point particleVisualizationSize;
        public Point ParticleVisualizationSize
        {
            get { return particleVisualizationSize; }
            set
            {
                if (PictureCoordinateConverter != null)
                    PictureCoordinateConverter.SetParticleWindowSize(value);
                particleVisualizationSize = value;
            }
        }
        public string CostName { get; set; }
        public IPictureCoordinateConverter PictureCoordinateConverter { get; set; }
    }
}