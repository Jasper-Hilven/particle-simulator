using ParticleSimulation.CostCalculation.CostStorage;
using ParticleSimulation.Sorting;
using ParticleSimulation.Sorting.Cost;
using ParticleSimulation.Structuring;
using ParticleSimulation.Tactics.KMeans.KMeansSectioning;

namespace ParticleSimulation.Tactics.KMeans.KMeansSorting
{
    public class MemLimKMeansSorterFactory : ISorterFactory
    {
        private readonly float growthParameter;
        public  MemLimKMeansSorterFactory(float growthParameter)
        {
            this.growthParameter = growthParameter;
        }

        public ISorter GetSorter(SortingCostCalculator calculator)
        {
            return new KMeansSorter(calculator, growthParameter, new BoundedSectionPositionUpdater(), new MemoryLimitDistTransPrep(growthParameter), new NeighbourSizeGetter(0),new NeighbourPositionGetter() );
        }
    }
}