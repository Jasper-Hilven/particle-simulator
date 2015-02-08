using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Emgu.CV;
using Emgu.CV.Structure;
using ParticleSimulation.Structuring;
using ParticleSimulation.Tactics.MatrixSort;
using ParticleSimulation.Tactics.SideSort;
using ParticleSimulation.Visualization.StepVisualization.PictureCombining.SubVisualization;

namespace ParticleSimulation.Visualization.StepVisualization.PictureCombining
{
    public class CombinedPictureVisualizer : IStepVisualizer
    {
        private readonly string outputPath;
        private readonly int height; //Z coordinate
        private readonly int width; //X coordinate
         public List<ISubVisualizer> Visualizers = new List<ISubVisualizer>();

        public CombinedPictureVisualizer(string outputPath, int height, int width)
        {
            if (!Directory.Exists(outputPath))
                Directory.CreateDirectory(outputPath);
            this.outputPath = outputPath;
            this.height = height;
            this.width = width;
         }

        public void VisualizeStep(SimulationStructure structure, int step)
        {
             if (!Directory.Exists(outputPath))
                Directory.CreateDirectory(outputPath);

            var image = new Image<Rgb, byte>(width, height);

            foreach (var subVisualizer in Visualizers)
            {
                subVisualizer.DrawVisualization(structure, image);
            }
            var bitmap = image.ToBitmap();
            bitmap.Save(outputPath + step + ".jpg");
            VisualizeMatrixStep(structure,step);
        }

        private void VisualizeMatrixStep(SimulationStructure structure, int step)
        {
            foreach (var particleSection in structure.GetAllSections())
            {
                if (!(particleSection is MatrixSortParticleSection))
                    return;
                var msec = particleSection as MatrixSortParticleSection;
                

            }
            var particleSections = structure.GetAllSections().ToList();
            var toPrintBuffer = new StringBuilder();
            for (int i = 0; i < particleSections.Count(); i++)
            {
                var lowerBound = particleSections[i].LowerBound;
                toPrintBuffer.AppendLine();
                toPrintBuffer.AppendLine("Section: " + lowerBound.X + "," + lowerBound.Y + "," + lowerBound.Z);
                toPrintBuffer.AppendLine();

                var mySec = particleSections[i] as MatrixSortParticleSection;
                var pDim = mySec.ParticlesMatrix.GetLength(0);
                for (int j = 0; j < 8; j++)
                {
                    var Xd = j%2 == 0 ? 0: pDim - 1;
                    var Yd = j%4 < 2 ? 0: pDim - 1;
                    var Zd = j < 4 ? 0: pDim - 1;
                    var particle = mySec.ParticlesMatrix[Xd, Yd, Zd];
                    toPrintBuffer.AppendLine("  Index: " + Xd + ","+ Yd + ","+ Zd);
                    var position = particle.Position;
                    toPrintBuffer.AppendLine("  Position: " + position.X + ","+ position.Y + ","+ position.Z);
                }
            }
            var path = outputPath +"cornerPartics" + step;
            File.WriteAllText(path,toPrintBuffer.ToString());
        }
    }
}
