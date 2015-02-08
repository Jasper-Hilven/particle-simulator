using System.Collections.Generic;
using System.Linq;
using ParticleSimulation.CostCalculation.CostReviewStorage;
using ParticleSimulation.CostCalculation.CostStorage;
using ParticleSimulation.Structuring;

namespace ParticleSimulation.CostCalculation.CostReview
{
    public class AllCostReviewer : IReviewCostStorage
    {
        private readonly IReviewCostStorage decoratedViewer;
        private ISet<ICostType> types = new HashSet<ICostType>();
        private HashSet<int> zeroCostAdded = new HashSet<int>();

        public AllCostReviewer(IReviewCostStorage decoratedViewer)
        {
            this.decoratedViewer = decoratedViewer;
        }

        public void AddCost(ICostType cType, ICore core, int step, double cost)
        {
            if (!types.Contains(cType))
                types.Add(cType);
            decoratedViewer.AddCost(cType, core, step, cost);
        }

        public Dictionary<ICore, Dictionary<ICostType, double>> GetCost(int step)
        {
            var unsortedSet = decoratedViewer.GetCost(step);
            if (!zeroCostAdded.Contains(step))
            {
                foreach (var costType in types)
                {
                    foreach (var coreCost in unsortedSet.Where(coreCost => !coreCost.Value.Keys.Contains(costType)))
                        coreCost.Value.Add(costType, 0d);
                }
                zeroCostAdded.Add(step);
                
                
            }
            return unsortedSet;
        }
        
    }
}