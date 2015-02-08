using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ParticleSimulation.Structuring;
using ParticleSimulation.Visualization.CostVisualization;
using ParticleSimulation.Visualization.StepVisualization.PictureCombining;

namespace ParticleSimulation.Visualization.StepVisualization
{
    class StepVisualizer: IStepVisualizer
    {
        private CostPrinter costPrinter;
        private MergedPictureVisualizer pictureVisualizer;
        public StepVisualizer(CostPrinter costPrinter, MergedPictureVisualizer pictureVisualizer)
        {
            this.costPrinter = costPrinter;
            this.pictureVisualizer = pictureVisualizer;
        }

        public void VisualizeStep(SimulationStructure structure, int step)
        {
            costPrinter.PrintCostToFile(step);
            pictureVisualizer.VisualizeStep(structure,step);
        }
    }
}
