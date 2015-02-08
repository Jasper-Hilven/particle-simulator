using System.Collections.Generic;

namespace ParticleSimulation.CostCalculation
{
    public interface ICostType
    {
        string GetCostName();
        
    }

    public class CostTypeComparer : IComparer<ICostType>
    {
        public int Compare(ICostType x, ICostType y)
        {
            return System.String.CompareOrdinal(x.GetCostName(), y.GetCostName());
        }
    }
}