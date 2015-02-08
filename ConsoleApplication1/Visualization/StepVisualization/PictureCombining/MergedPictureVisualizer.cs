using ParticleSimulation.Structuring;

namespace ParticleSimulation.Visualization.StepVisualization.PictureCombining
{
    class MergedPictureVisualizer : IStepVisualizer
    {
        private readonly PictureMerger pictureMerger;
        private readonly CombinedPictureVisualizer combinedVisualizer;
        private readonly int snapTime;
        public MergedPictureVisualizer(PictureMerger pictureMerger, CombinedPictureVisualizer combinedVisualizer, int snapTime)
        {
            this.pictureMerger = pictureMerger;
            this.combinedVisualizer = combinedVisualizer;
            this.snapTime = snapTime;
        }

        public void VisualizeStep(SimulationStructure structure, int step)
        {
            if ((step + 1)%snapTime != 0) return;
            combinedVisualizer.VisualizeStep(structure, step);
            pictureMerger.CreatePicture(step);
        }
    }
}
