using ParticleSimulation.CostCalculation.CostStorage;

namespace ParticleSimulation.FieldCalculation.Cost
{
    public class CohesionComCalcFactory:IComCostCalculatorFactory
    {
        public IFieldComCostCalculator GetCalculator(ICostStorage storage, int cellsPerSection, int particleReductionFactor)
        {
            return new CohesionComCalculator(storage,cellsPerSection);//Ignores particleReductionFactor
        }
    }
}