using System;
using ParticleSimulation.Visualization.CostVisualization;

namespace ParticleSimulation.Visualization.FinalVisualization
{
    public class FinalVisualizer : IFinalVisualizer
    {
        private VideoMaker videoMaker;
        private int snapTime;
        private CostPrinter costPrinter;

        public FinalVisualizer(VideoMaker videoMaker, int snapTime, CostPrinter costPrinter)
        {
            this.videoMaker = videoMaker;
            this.snapTime = snapTime;
            this.costPrinter = costPrinter;
        }

        public void Visualize(int lastStep)
        {
            videoMaker.CreateVideo(lastStep, snapTime);
            //costPrinter.PrintCostToFile(lastStep);
        }


    }
}