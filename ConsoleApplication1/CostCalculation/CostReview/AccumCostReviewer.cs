using System.Collections.Generic;
using ParticleSimulation.CostCalculation.CostStorage;
using ParticleSimulation.Structuring;

namespace ParticleSimulation.CostCalculation.CostReview
{
    /// <summary>
    /// Accumulates all costs over time
    /// </summary>
    public class AccumCostReviewer : ICostReviewer
    {
        private readonly ICostReviewer storage;

        public AccumCostReviewer(ICostReviewer storage)
        {
            this.storage = storage;
        }

        public Dictionary<ICore, Dictionary<ICostType, double>> GetCost(int step)
        {
            var accumCost = new Dictionary<ICore, Dictionary<ICostType,double>>();
            for (var i = 0; i <= step; i++)
            {
                var costs = storage.GetCost(i);
                foreach (var coreCostPair in costs)
                {
                    if(!accumCost.ContainsKey(coreCostPair.Key))
                        accumCost.Add(coreCostPair.Key,new Dictionary<ICostType, double>());
                    foreach (var typeCostPair in coreCostPair.Value)
                    {
                        var coreDict = accumCost[coreCostPair.Key];
                        if(!coreDict.ContainsKey(typeCostPair.Key))
                            coreDict.Add(typeCostPair.Key,0);
                        var curVal = coreDict[typeCostPair.Key];
                        coreDict.Remove(typeCostPair.Key);
                        coreDict.Add(typeCostPair.Key,curVal+typeCostPair.Value);
                    }
                }
            }
            return accumCost;
        }
    }
}