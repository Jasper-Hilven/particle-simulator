using ParticleSimulation.CostCalculation;

namespace ParticleSimulation.Sorting.Cost
{
    public class SortingCostReceiveParticle : ICostType
    {
        public string GetCostName()
        {
            return "SortingCostReceiveParticle";
        }

        public override bool Equals(object obj)
        {
            if (obj == null)
                return false;
            return obj is SortingCostReceiveParticle;
        }
        public override int GetHashCode()
        {
            return 23552544;
        }

    }
}