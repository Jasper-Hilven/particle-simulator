using System;
using System.IO;
using System.Text;
using ParticleSimulation.CostCalculation.CostReview;
using ParticleSimulation.CostCalculation.CostStorage;

namespace ParticleSimulation.Visualization.CostVisualization
{
    /// <summary>
    /// Writes the costs to a file.
    /// </summary>
    public class CostPrinter
    {
        private readonly string printPath;
        private readonly ICostReviewer storage;
        private string commonFileName;

        public CostPrinter(string printPath, ICostReviewer storage,string commonFileName)
        {
            this.printPath = printPath;
            this.storage = storage;
            this.commonFileName = commonFileName;
        }

        public void PrintCostToFile(int step)
        {
            var toPrint = GetPrintText(step);
            if (!Directory.Exists(printPath))
                Directory.CreateDirectory(printPath);

            File.WriteAllText(printPath + commonFileName + "_" + step + ".txt", toPrint);
        }

        private string GetPrintText( int step)
        {
            var build = new StringBuilder();
            foreach (var cost in storage.GetCost(step))
            {
                build.AppendLine("Core");
                var coreCosts = cost.Value;
                var sum = 0d;
                
                foreach (var coreCost in coreCosts)
                {
                    if(coreCosts.Count > 4)
                        throw new Exception();
                    build.AppendLine("  " + coreCost.Key.GetCostName());
                    build.AppendLine("    " + coreCost.Value);
                    build.AppendLine("");
                    sum += coreCost.Value;
                }
                build.AppendLine("  CoreTotal");
                build.AppendLine("    "+ sum);
                build.AppendLine("");
           
            }
            return build.ToString();
        }

    }
}
