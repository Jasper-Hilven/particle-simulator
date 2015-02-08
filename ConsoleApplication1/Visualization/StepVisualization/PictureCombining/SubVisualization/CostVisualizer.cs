using System.Collections.Generic;
using System.Drawing;
using ParticleSimulation.CostCalculation;
using ParticleSimulation.CostCalculation.CostReview;
using ParticleSimulation.CostCalculation.CostStorage;

namespace ParticleSimulation.Visualization.StepVisualization.PictureCombining.SubVisualization
{
    public class CostVisualizer
    {
        private readonly ICostReviewer showStorage;
        private Point size;
        private readonly string name;
        public CostVisualizer(ICostReviewer showStorage, Point size, string name)
        {
            this.showStorage = showStorage;
            this.size = size;
            this.name = name;
        }

        public Bitmap GetCostOfStep(int step)
        {
            var retMap = new Bitmap(size.X, size.Y);
            var drawGraph = Graphics.FromImage(retMap);
            var titleFont = new Font("Times New Roman", 16);
            var smallFont = new Font("Times New Roman", 12);
            var solidBrush = new SolidBrush(Color.Red);
            drawGraph.DrawString(name, titleFont, solidBrush, 0, 0);
            var currentPosition = new Point(2, 30);
            foreach (var costs in showStorage.GetCost(step))
            {
                var unsort = costs.Value;
                var toAdd = new SortedDictionary<ICostType, double>(new CostTypeComparer());
                foreach (var d in unsort)
                {
                    toAdd.Add(d.Key, d.Value);
                }
                foreach (var d in toAdd)
                {
                    drawGraph.DrawString(d.Key.GetCostName(), smallFont, solidBrush, currentPosition.X, currentPosition.Y);

                    const int newX = 220;
                    drawGraph.DrawString("" + d.Value, smallFont, solidBrush, newX, currentPosition.Y);
                    currentPosition = new Point(2, currentPosition.Y + 15);
                }
            }
            drawGraph.DrawString("Step: " + step, smallFont, solidBrush, 0, currentPosition.Y);
             

            /*
             
             var sortedCost = new Dictionary<ICore, SortedDictionary<ICostType, double>>();
                foreach (var VARIABLE in unsortedSet)
                {
                    var toAdd = new SortedDictionary<ICostType, double>();
                    
                    foreach (var kPair in VARIABLE.Value)
                    {
                        toAdd.Add(kPair.Key,kPair.Value);
                    }
                    sortedCost.Add(VARIABLE.Key,toAdd);
                }
             */
            drawGraph.Dispose();
            return retMap;
        }
    }
}