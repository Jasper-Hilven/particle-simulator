using ParticleSimulation.CostCalculation;
using ParticleSimulation.CostCalculation.CostStorage;
using ParticleSimulation.Sorting;
using ParticleSimulation.Sorting.Cost;

namespace ParticleSimulation.Structuring
{
    public interface IParticleTransmitterFactory
    {
        IParticleTransmitter GetParticleTransmitter(SortingCostCalculator costCalculator);
    }
}