using System;
using System.Collections.Generic;
using ParticleSimulation.Structuring;
using ParticleSimulation.Structuring.Sectioning;

namespace ParticleSimulation.CostCalculation.CostReviewStorage
{
    /***
     *Only stores the total cost.
     */
    public class TotalCostStorage:IReviewCostStorage
    {
        private readonly CostStorage decoratedStorage;

        public TotalCostStorage()
        {
            decoratedStorage = new CostStorage();
        }

        private readonly ICore  totalCore = new TotalCore();
        public void AddCost(ICostType cType, ICore core, int step, double cost)
        {
            decoratedStorage.AddCost(cType,totalCore,step,cost);
        }

        public class TotalCore : ICore
        {
            public IEnumerable<IParticleSection> Sections { get{return new List<IParticleSection>() ; } }
            public void AddSection(IParticleSection particleSection)
            {
                throw new Exception("Should not be used!!");
            }
        }

        public Dictionary<ICore, Dictionary<ICostType, double>> GetCost(int step)
        {
            return decoratedStorage.GetCost(step);
        }
    }
    
}