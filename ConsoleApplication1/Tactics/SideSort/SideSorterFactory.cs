using ParticleSimulation.Sorting;
using ParticleSimulation.Sorting.Cost;
using ParticleSimulation.Structuring;

namespace ParticleSimulation.Tactics.SideSort
{
    public class SideSorterFactory:ISorterFactory
    {
        public SideSorterFactory()
        {
        }

        public ISorter GetSorter(SortingCostCalculator calculator)
        {
            return new Sorter();
        }
    }
}