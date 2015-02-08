using ParticleSimulation.CostCalculation;
using ParticleSimulation.CostCalculation.CostStorage;
using ParticleSimulation.Sorting;
using ParticleSimulation.Sorting.Cost;
using ParticleSimulation.Structuring;

namespace ParticleSimulation.Tactics.Helsim
{
    public class HelsimSorterFactory:ISorterFactory
    {
        public ISorter GetSorter(SortingCostCalculator calculator)
        {
            return new HelsimSorter(calculator);
        }
    }
}