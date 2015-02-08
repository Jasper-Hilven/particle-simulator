using System.Collections.Generic;
using ParticleSimulation.CostCalculation.CostReview;
using ParticleSimulation.Structuring;

namespace ParticleSimulation.CostCalculation.CostStorage
{

    /// <summary>
    /// Only remembers one step.
    /// </summary>
    public class SmallCostStorage:ICostReviewer

{
        public Dictionary<int, Dictionary<ICore, Dictionary<ICostType, double>>> Costs { get { return decStorage.Costs; } }
        private CostReviewStorage.CostStorage decStorage = new CostReviewStorage.CostStorage();
        private int currentStep = 0;
        public void AddCost(ICostType cType, ICore core, int step, double cost)
        {
            if(step != currentStep)
            {
                decStorage = new CostReviewStorage.CostStorage();
                currentStep = step;
            }
            decStorage.AddCost(cType,core,step,cost);
        }

        public Dictionary<ICore, Dictionary<ICostType, double>> GetCost(int step)
        {
            return decStorage.Costs[step];
        }
}
}