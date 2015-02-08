using ParticleSimulation.CostCalculation;

namespace ParticleSimulation.Sorting.Cost
{
    /*public class SortingCostParticleCalculation : ICostType
    {
        public string GetCostName()
        {
            return "SortingCostParticleCalculation";
        }
        public override int GetHashCode()
        {
            return 362526;
        }

        public override bool Equals(object obj)
        {
            if (obj == null)
                return false;
            return obj is SortingCostParticleCalculation;
        }
    }
     * */
    public class SortingCostParticleSwap: ICostType
    {
        public string GetCostName()
        {
            return "SortingCostParticleSwap";
        }
        public override int GetHashCode()
        {
            return 362526;
        }

        public override bool Equals(object obj)
        {
            if (obj == null)
                return false;
            return obj is SortingCostParticleSwap;
        }
    }
    public class SortingCostParticleComparison: ICostType
    {
        public string GetCostName()
        {
            return "SortingCostParticleSwap";
        }
        public override int GetHashCode()
        {
            return 362526;
        }

        public override bool Equals(object obj)
        {
            if (obj == null)
                return false;
            return obj is SortingCostParticleComparison;
        }
    }
}