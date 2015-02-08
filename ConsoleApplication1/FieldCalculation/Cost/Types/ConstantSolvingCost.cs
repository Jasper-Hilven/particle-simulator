using ParticleSimulation.CostCalculation;

namespace ParticleSimulation.FieldCalculation.Cost.Types
{
    public class ConstantSolvingCost : ICostType
     {

        private static ConstantSolvingCost csc = new ConstantSolvingCost();

        public static ConstantSolvingCost GetConstantSolvingCost()
        {
            return csc;
        }

    
        private ConstantSolvingCost()
        {
        }

        public string GetCostName()
        {
            return "Constant solving cost:";
        }
    }
}