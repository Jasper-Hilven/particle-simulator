using ParticleSimulation.CostCalculation;
using ParticleSimulation.CostCalculation.CostStorage;
using ParticleSimulation.Sorting;
using ParticleSimulation.Sorting.Cost;
using ParticleSimulation.Structuring;

namespace ParticleSimulation.Tactics.MatrixSort
{
    public class MatrixSorterFactory : ISorterFactory
    {
        
        private int timesSorting;
        private int perTimeSorting;

        public MatrixSorterFactory(int timesSorting, int perTimeSorting)
        {
            this.timesSorting = timesSorting;
            this.perTimeSorting = perTimeSorting;
        }

        public ISorter GetSorter(SortingCostCalculator calculator)
        {
            return new MatrixSorter(calculator,timesSorting,perTimeSorting);
        }
        
    }
}