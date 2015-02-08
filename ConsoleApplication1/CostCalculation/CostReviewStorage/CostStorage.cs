using System;
using System.Collections.Generic;
using ParticleSimulation.Structuring;

namespace ParticleSimulation.CostCalculation.CostReviewStorage
{
    public class CostStorage:IReviewCostStorage
    {
        public readonly Dictionary<int, Dictionary<ICore, Dictionary<ICostType, double>>> costs = new Dictionary<int, Dictionary<ICore, Dictionary<ICostType, double>>>();

        public Dictionary<int, Dictionary<ICore, Dictionary<ICostType, double>>> Costs { get { return costs; } }

        public void AddCost(ICostType cType, ICore core, int step, double cost)
        {
            {
                if(!costs.ContainsKey(step))
                    costs.Add(step,new Dictionary<ICore, Dictionary<ICostType, double>>());

                var stepD = costs[step];
                if(!stepD.ContainsKey(core))
                    stepD.Add(core,new Dictionary<ICostType, double>());
                var coreD = stepD[core];
                if(!coreD.ContainsKey(cType))
                    coreD.Add(cType,0f);
                    coreD[cType] += cost;
               
            }         

        }

        public Dictionary<ICore, Dictionary<ICostType, double>> GetCost(int step)
        {
            return costs[step];
        }
    }
}