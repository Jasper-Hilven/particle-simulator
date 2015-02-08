using System;
using System.Collections.Generic;
using System.Drawing;
using ParticleSimulation.CostCalculation.CostReview;
using ParticleSimulation.CostCalculation.CostStorage;
using ParticleSimulation.Visualization.CostVisualization;
using ParticleSimulation.Visualization.FinalVisualization;
using ParticleSimulation.Visualization.StepVisualization;
using ParticleSimulation.Visualization.StepVisualization.PictureCombining;
using ParticleSimulation.Visualization.StepVisualization.PictureCombining.SubVisualization;

namespace ParticleSimulation.Visualization.Initialization
{
    public class VisualizationBuilder : IVisualizationBuilder
    {

        private readonly VisualizationParameters parameters;
        //private Point totalSize;

        public VisualizationBuilder(VisualizationParameters visualizationParameters)
        {
            parameters = visualizationParameters;
        }


        public IStepVisualizer GetStepVisualizer(ICostReviewer storage)
        {
            var picturePartMakers = new List<PicturePartMaker>();
            var costVisualizationSize = parameters.CostVisualizationSize;
            var costVisualizer = new CostVisualizer(storage, costVisualizationSize, parameters.CostName);
            picturePartMakers.Add(new PicturePartMaker()
                                  {
                                      StartCoordinates = new Point(0, 0),
                                      StepDrawer =
                                          (int i) =>
                                          {
                                              return
                                                  (Bitmap)
                                                      Image.FromFile(getMainPath() + "ParticlePictures\\" + i + ".jpg");
                                          }
                                  });
            picturePartMakers.Add(new PicturePartMaker()
                                  {
                                      StartCoordinates =
                                          new Point(0, getParticleVisualizationSize().Y),
                                      StepDrawer = costVisualizer.GetCostOfStep
                                  });
            var totalSize = new Point(Math.Max(getParticleVisualizationSize().X, costVisualizationSize.X),
                getParticleVisualizationSize().Y + costVisualizationSize.Y);
            var pictureMerger = new PictureMerger(getMainPath() + "MergedPictures\\", picturePartMakers, totalSize);
            var daPictureOutput = new CombinedPictureVisualizer(getMainPath() + "ParticlePictures\\",
                getParticleVisualizationSize().Y, getParticleVisualizationSize().X)
                                  {
                                      Visualizers =
                                          parameters
                                          .ParticleSubVisualizers
                                  };
            var mergedPicViz = new MergedPictureVisualizer(pictureMerger, daPictureOutput, parameters.SnapTime);
            var costPrinter = new CostPrinter(getMainPath() + "CostPrints\\", storage, "");
            return new StepVisualizer(costPrinter, mergedPicViz);
        }

        public IFinalVisualizer GetFinalVisualizer(ICostReviewer storage)
        {
            var costVisualizationSize = parameters.CostVisualizationSize;
            var totalSize = new Point(Math.Max(getParticleVisualizationSize().X, costVisualizationSize.X),
                getParticleVisualizationSize().Y + costVisualizationSize.Y);
            var videoMaker = new VideoMaker(getMainPath() + "MergedPictures\\", getMainPath() + "FinalVideo\\",
                parameters.VideoName + ".avi", totalSize);
            var accumAllToOneCost = new AccumCostReviewer(new SingleCoreCostReviewer(storage));
            var finalVis = new FinalVisualizer(videoMaker, parameters.SnapTime,
                new CostPrinter(getMainPath() + "CostPrints\\", accumAllToOneCost, "FINAL"));
            return finalVis;
        }


        private string getMainPath()
        {
            return parameters.MainOutputPath;
        }

        private Point getParticleVisualizationSize()
        {
            return parameters.ParticleVisualizationSize;
        }

    }
    public class CommonVisualizationBuilder : IVisualizationBuilder
    {

        private readonly VisualizationParameters parameters;
        //private Point totalSize;

        public CommonVisualizationBuilder(VisualizationParameters visualizationParameters)
        {
            parameters = visualizationParameters;
        }


        public IStepVisualizer GetStepVisualizer(ICostReviewer storage)
        {
            var picturePartMakers = new List<PicturePartMaker>();
            var costVisualizationSize = parameters.CostVisualizationSize;
            var costVisualizer = new CostVisualizer(storage, costVisualizationSize, parameters.CostName);
            picturePartMakers.Add(new PicturePartMaker()
            {
                StartCoordinates = new Point(0, 0),
                StepDrawer =
                    (int i) =>
                    {
                        return
                            (Bitmap)
                                Image.FromFile(getMainPath() + "ParticlePictures\\" + i + ".jpg");
                    }
            });
            picturePartMakers.Add(new PicturePartMaker()
            {
                StartCoordinates =
                    new Point(0, getParticleVisualizationSize().Y),
                StepDrawer = costVisualizer.GetCostOfStep
            });
            var totalSize = new Point(Math.Max(getParticleVisualizationSize().X, costVisualizationSize.X),
                getParticleVisualizationSize().Y + costVisualizationSize.Y);
            var pictureMerger = new PictureMerger(getMainPath() + "MergedPictures\\", picturePartMakers, totalSize);
            var daPictureOutput = new CombinedPictureVisualizer(getMainPath() + "ParticlePictures\\",
                getParticleVisualizationSize().Y, getParticleVisualizationSize().X)
            {
                Visualizers =
                    parameters
                    .ParticleSubVisualizers
            };
            var mergedPicViz = new MergedPictureVisualizer(pictureMerger, daPictureOutput, parameters.SnapTime);
            var costPrinter = new CostPrinter(getMainPath() + "CostPrints\\", storage, "");
            return new StepVisualizer(costPrinter, mergedPicViz);
        }

        public IFinalVisualizer GetFinalVisualizer(ICostReviewer storage)
        {
            var costVisualizationSize = parameters.CostVisualizationSize;
            var totalSize = new Point(Math.Max(getParticleVisualizationSize().X, costVisualizationSize.X),
                getParticleVisualizationSize().Y + costVisualizationSize.Y);
            var videoMaker = new VideoMaker(getMainPath() + "MergedPictures\\", getMainPath() + "FinalVideo\\",
                parameters.VideoName + ".avi", totalSize);
            var accumAllToOneCost = new AccumCostReviewer(new SingleCoreCostReviewer(storage));
            var finalVis = new FinalVisualizer(videoMaker, parameters.SnapTime,
                new CostPrinter(getMainPath() + "CostPrints\\", accumAllToOneCost, "FINAL"));
            return finalVis;
        }


        private string getMainPath()
        {
            return parameters.MainOutputPath;
        }

        private Point getParticleVisualizationSize()
        {
            return parameters.ParticleVisualizationSize;
        }

    }
}