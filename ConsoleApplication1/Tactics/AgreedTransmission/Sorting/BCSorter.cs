using ParticleSimulation.Sorting;
using ParticleSimulation.Structuring;
using ParticleSimulation.Tactics.KMeans.KMeansSectioning;

namespace ParticleSimulation.Tactics.AgreedTransmission.Sorting
{
    public class BcSorter : ISorter
    {
        private readonly BridgeInterestGetter bridgeInterestGetter;
        private readonly BridgeTransmissionPreparer bridgeTransmissionPreparer;
        private readonly NeighbourPositionGetter neighbourPositionGetter;
        private readonly ISectionPositionUpdater sectionPositionUpdater;
        private readonly NeighbourTransmitter transmitter;

        public BcSorter(NeighbourPositionGetter neighbourPositionGetter, BridgeInterestGetter bridgeInterestGetter,
            BridgeTransmissionPreparer bridgeTransmissionPreparer, ISectionPositionUpdater sectionPositionUpdater,
            NeighbourTransmitter transmitter)
        {
            this.neighbourPositionGetter = neighbourPositionGetter;
            this.bridgeInterestGetter = bridgeInterestGetter;
            this.bridgeTransmissionPreparer = bridgeTransmissionPreparer;
            this.transmitter = transmitter;
            this.sectionPositionUpdater = sectionPositionUpdater;
        }

        public void Sort(SimulationStructure simulationStructure, int step)
        {
            sectionPositionUpdater.UpdateSectionPositions(simulationStructure);
            // puts right amount of particles for transmission calculates the total transmission space needed.
            bridgeTransmissionPreparer.PutParticlesForTransmission(simulationStructure);
            transmitter.TransmitParticles(simulationStructure, step);
            neighbourPositionGetter.GetOldPositionsNeighbours(simulationStructure);
            //Calculate new increase and send and give to neighbours.
            bridgeInterestGetter.GetOldNeighbourInterests(simulationStructure);
        }
    }
}