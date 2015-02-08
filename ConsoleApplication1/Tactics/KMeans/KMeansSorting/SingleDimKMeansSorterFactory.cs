using ParticleSimulation.CostCalculation;
using ParticleSimulation.CostCalculation.CostStorage;
using ParticleSimulation.Sorting;
using ParticleSimulation.Sorting.Cost;
using ParticleSimulation.Structuring;
using ParticleSimulation.Tactics.KMeans.KMeansSectioning;

namespace ParticleSimulation.Tactics.KMeans.KMeansSorting
{
    public class SingleDimKMeansSorterFactory : ISorterFactory
    {
        //GrowthParameter means that if you have less particles than your neigbour, particles are closer to you.
        private readonly float growthParameter;
        public SingleDimKMeansSorterFactory(float growthParameter)
        {
            this.growthParameter = growthParameter;
        }

        public ISorter GetSorter(SortingCostCalculator calculator)
        {
            return new KMeansSorter(calculator, growthParameter, new BoundedSectionPositionUpdater(), new OneSideSingleDimensionTransmissionPreparer(growthParameter), new NeighbourSizeGetter(1), new NeighbourPositionGetter());
        }
    }
    public class KMeansSorterFactory : ISorterFactory
    {
        private readonly float growthParameter;
        public KMeansSorterFactory(float growthParameter)
        {
            this.growthParameter = growthParameter;
        }

        public ISorter GetSorter(SortingCostCalculator calculator)
        {
            return new KMeansSorter(calculator, growthParameter, new BoundedSectionPositionUpdater(), new DistanceTransmissionPreparer(growthParameter), new NeighbourSizeGetter(1), new NeighbourPositionGetter());
        }
    }

}