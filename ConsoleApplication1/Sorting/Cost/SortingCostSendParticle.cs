using ParticleSimulation.CostCalculation;

namespace ParticleSimulation.Sorting.Cost
{
    public class SortingCostSendParticle : ICostType
    {   
        public string GetCostName()
        {
            return "SortingCostSendParticle";
        }
        public override int GetHashCode()
        {
            return 523552545;
        }

        public override bool Equals(object obj)
        {
            if (obj == null)
                return false;
            return obj is SortingCostSendParticle;
        }
    }
}