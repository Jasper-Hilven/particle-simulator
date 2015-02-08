using ParticleSimulation.CostCalculation;
using ParticleSimulation.CostCalculation.CostStorage;
using ParticleSimulation.Sorting;
using ParticleSimulation.Sorting.Cost;
using ParticleSimulation.Structuring;

namespace ParticleSimulation.Tactics.OuterList
{
    public class StrictBoundarySorterFactory:ISorterFactory
    {

        public ISorter GetSorter(SortingCostCalculator calculator)
        {
            return new StrictBoundarySorter(calculator);
        }
    }
}