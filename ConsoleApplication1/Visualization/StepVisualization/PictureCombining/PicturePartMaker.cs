using System;
using System.Drawing;

namespace ParticleSimulation.Visualization.StepVisualization.PictureCombining
{
    public class PicturePartMaker
    {
        public Func<int, Bitmap> StepDrawer;
        public Point StartCoordinates; 
    }
}