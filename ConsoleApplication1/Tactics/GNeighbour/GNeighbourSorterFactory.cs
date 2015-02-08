using ParticleSimulation.CostCalculation.CostStorage;
using ParticleSimulation.Sorting;
using ParticleSimulation.Sorting.Cost;
using ParticleSimulation.Structuring;

namespace ParticleSimulation.Tactics.GNeighbour
{
    public class GNeighbourSorterFactory:ISorterFactory
    {
        private readonly float centerFreeDistance;

        public GNeighbourSorterFactory(float centerFreeDistance)
        {
            this.centerFreeDistance = centerFreeDistance;
        }

        public ISorter GetSorter(SortingCostCalculator calculator)
        {
            return new GNeighbourSorter(calculator, centerFreeDistance);
        }
    }
}