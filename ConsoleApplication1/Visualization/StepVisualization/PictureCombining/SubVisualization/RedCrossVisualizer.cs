using System.Drawing;
using Emgu.CV;
using Emgu.CV.Structure;
using ParticleSimulation.Structuring;

namespace ParticleSimulation.Visualization.StepVisualization.PictureCombining.SubVisualization
{
    public class RedCrossVisualizer:ISubVisualizer
    {
        public void DrawVisualization(SimulationStructure structure, Image<Rgb, byte> image)
        {
            for (var i = 0; i < image.Size.Height; i++)
            {
                var w = ((int)(((double) i)/((double)image.Size.Height)*image.Size.Width));
                if(w == image.Size.Width)
                    w--;
                var wComp = image.Size.Width - 1 - w;
                image[i, w] = new Rgb(Color.Red);
                image[i,wComp] = new Rgb(Color.Red);
            }
        }
    }
}