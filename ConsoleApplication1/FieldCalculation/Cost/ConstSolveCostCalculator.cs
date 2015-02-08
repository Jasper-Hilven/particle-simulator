using ParticleSimulation.CostCalculation;
using ParticleSimulation.CostCalculation.CostStorage;
using ParticleSimulation.FieldCalculation.Cost.Types;
using ParticleSimulation.Structuring;

namespace ParticleSimulation.FieldCalculation.Cost
{
    public class ConstSolveCostCalculator
    {
        private readonly ICostStorage storage;
        private readonly ICostType constantCost = ConstantSolvingCost.GetConstantSolvingCost();
        const double ConstantSolverCost = 0.0536490667;
        const double ConstantElecFieldCost = 0.0009930333;
       
        public ConstSolveCostCalculator(ICostStorage storage)
        {
            this.storage = storage;
        }

        public void AddCostForCore(ICore core, int step)
        {
            storage.AddCost(constantCost,core,step,ConstantSolverCost+ConstantElecFieldCost);
        }
    }
}