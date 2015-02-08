using System.Collections.Generic;
using System.Drawing;
using ParticleSimulation.Visualization.Initialization;
using ParticleSimulation.Visualization.StepVisualization.PictureCombining;
using ParticleSimulation.Visualization.StepVisualization.PictureCombining.SubVisualization;

namespace ParticleSimulation.Settings.HelperSettings
{
    public class VisualizationSettingsFactory
    {
        public VisualizationParameters GetVisualizationParameters()
        {
            var particleWindowSize = new Point(400, 400);
            IPictureCoordinateConverter pictureCoordinateConverter = new PictureCoordinateConverter(particleWindowSize);
            var subList = new List<ISubVisualizer> { new ParticleVisualizer(pictureCoordinateConverter)/*, new RedCrossVisualizer()*/ };
            var retParam = new VisualizationParameters
                               {
                                   CostName = "Undefined",
                                   CostVisualizationSize = new Point(400, 200),
                                   //CostVisualizerFactory = new CostVisualizerFactory(),
                                   MainOutputPath = @"C:\Users\Jasper\Documents\School\Thesis\OutputRuns\StandardOutput",
                                   MergedPictureAddPath = "MergedPath\\",
                                   ParticleSubVisualizers = subList,
                                   ParticleVisualizationSize = particleWindowSize,
                                   SnapTime = 5,
                                   PictureCoordinateConverter  = pictureCoordinateConverter,
                                   VideoAddPath = "Video\\",
                                   VideoName = "Video"
                               };
            return retParam;
        }
    }
}