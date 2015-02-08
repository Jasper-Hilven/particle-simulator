using ParticleSimulation.Sorting;
using ParticleSimulation.Sorting.Cost;
using ParticleSimulation.Structuring;

namespace ParticleSimulation.Tactics.AgreedTransmission.Sorting
{
    public class BcSorterFactory : ISorterFactory
    {
        public ISorter GetSorter(SortingCostCalculator calculator)
        {
            return new BcSorter(new NeighbourPositionGetter(), new BridgeInterestGetter(),
                new BridgeTransmissionPreparer(), new BoundedSectionPositionUpdater(),
                new NeighbourTransmitter(calculator));
        }
    }
}