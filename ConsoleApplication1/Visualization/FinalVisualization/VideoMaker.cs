using System;
using System.Drawing;
using System.IO;
using System.Threading;
using Emgu.CV;
using Emgu.CV.Structure;

namespace ParticleSimulation.Visualization.FinalVisualization
{
    public class VideoMaker
    {
        private string inputPath;
        private string outputPath;
        private string videoName;
        private int width;
        private int height;

        public VideoMaker(string inputPath, string outputPath, string videoName,Point totalSize)
        {
            this.inputPath = inputPath;
            this.outputPath = outputPath;
            this.videoName = videoName;
            width = totalSize.X;
            height = totalSize.Y;
        }

        public void CreateVideo(int lastStep, int snapTime)
        {
            if (!Directory.Exists(outputPath))
                Directory.CreateDirectory(outputPath);
            var a = new VideoWriter(outputPath + videoName,Emgu.CV.CvInvoke.CV_FOURCC('M','J','P','G'),30, width, height, true);

            for (var i = 0; i <= lastStep; i++)
            {
                if ((i + 1) % 100 == 0)
                    GC.Collect();
                if ((i + 1) % 50 == 0)
                System.Console.WriteLine("making movie: " + i);
           
                if ((i + 1) % snapTime != 0)
                    continue;
                var addPic = new Bitmap(Image.FromFile(inputPath + i + ".jpg"));
                a.WriteFrame(new Image<Rgb, byte>(addPic));
                addPic.Dispose();
                Thread.Sleep(20);
            }

            a.Dispose();


        }
    }
}