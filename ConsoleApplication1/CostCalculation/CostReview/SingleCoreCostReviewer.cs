using System;
using System.Collections.Generic;
using System.Linq;
using ParticleSimulation.CostCalculation.CostStorage;
using ParticleSimulation.Structuring;
using ParticleSimulation.Structuring.Sectioning;

namespace ParticleSimulation.CostCalculation.CostReview
{
    //All costs belong to one core, according to GetCost
    public class SingleCoreCostReviewer : ICostReviewer
    {

        private readonly ICostReviewer decoCostStorage;
        private readonly ICore singeCostCore = new SingleCostCore();
        public SingleCoreCostReviewer(ICostReviewer decoCostStorage)
        {
            this.decoCostStorage = decoCostStorage;
        }

        
        private class SingleCostCore : ICore
        {
            public IEnumerable<IParticleSection> Sections { get; private set;}
            public void AddSection(IParticleSection particleSection)
            {
                throw new Exception("Should not add section to fictive core");
            }
        }

        public Dictionary<ICore, Dictionary<ICostType, double>> GetCost(int step)
        {
            var decoResult = decoCostStorage.GetCost(step);
            var retDic = new Dictionary<ICore, Dictionary<ICostType, double>>();
            retDic.Add(singeCostCore,new Dictionary<ICostType, double>());
            foreach (var d in decoResult.SelectMany(CoreCostPair => CoreCostPair.Value))
            {
                if (!retDic[singeCostCore].ContainsKey(d.Key))
                {
                    retDic[singeCostCore].Add(d.Key,d.Value);
                    continue;
                }
                var value = retDic[singeCostCore][d.Key];
                retDic[singeCostCore].Remove(d.Key);
                retDic[singeCostCore].Add(d.Key,value+d.Value);
            }
            return retDic;
        }
    }
}