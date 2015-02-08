using ParticleSimulation.CostCalculation.CostStorage;

namespace ParticleSimulation.FieldCalculation.Cost
{
    public interface IComCostCalculatorFactory
    {
        IFieldComCostCalculator GetCalculator(ICostStorage storage, int cellsPerSection, int particleReductionFactor);
    }
}