using System.Collections.Generic;
using ParticleSimulation.Structuring;

namespace ParticleSimulation.CostCalculation.CostStorage
{
    public class CombinedStorage:ICostStorage
    {
        public List<ICostStorage> CostStorages = new List<ICostStorage>();
        public void AddCost(ICostType cType, ICore core, int step, double cost)
        {
            foreach (var costStorage in CostStorages)
            {
                costStorage.AddCost(cType,core,step,cost);
            }
        }

    }
}