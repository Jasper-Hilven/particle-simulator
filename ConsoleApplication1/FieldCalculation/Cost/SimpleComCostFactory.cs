using ParticleSimulation.CostCalculation.CostStorage;

namespace ParticleSimulation.FieldCalculation.Cost
{
    public class SimpleComCostFactory:IComCostCalculatorFactory
    {
        public IFieldComCostCalculator GetCalculator(ICostStorage storage, int nbParticlesPerCell, int particleReductionFactor)
        {
            return new SimpleComCalculator(storage,particleReductionFactor);
        }
    }
}