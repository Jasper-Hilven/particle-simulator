using System.Collections.Generic;
using System.Drawing;
using System.IO;

namespace ParticleSimulation.Visualization.StepVisualization.PictureCombining
{
    /**Merges multiple pictures together into 1 picture. This can be used for later video output*/
    public class PictureMerger
    {
        private string outputPath;
        private List<PicturePartMaker> PicturePartMakers;
        private Point totalSize;

        public PictureMerger(string outputPath, List<PicturePartMaker> picturePartMakers, Point totalSize)
        {
            this.outputPath = outputPath;
            PicturePartMakers = picturePartMakers;
            this.totalSize = totalSize;
        }

        public void CreatePicture(int step)
        {
            var final = new Bitmap(totalSize.X, totalSize.Y);
            var g = Graphics.FromImage(final);
            foreach (var picturePartMaker in PicturePartMakers)
            {
                var subPicture = picturePartMaker.StepDrawer(step);
                g.DrawImage(subPicture,picturePartMaker.StartCoordinates);
                subPicture.Dispose();
            }
            if (!Directory.Exists(outputPath))
                Directory.CreateDirectory(outputPath);
            final.Save(outputPath + step+ ".jpg");
            final.Dispose();
             
        }
    }
}