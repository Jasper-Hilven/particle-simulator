using ParticleSimulation.CostCalculation;
using ParticleSimulation.CostCalculation.CostStorage;
using ParticleSimulation.Sorting;
using ParticleSimulation.Sorting.Cost;
using ParticleSimulation.Structuring;

namespace ParticleSimulation.Tactics.Helsim
{
    public class HelsimSorter: ISorter
    {
        private SortingCostCalculator costCalculator;
        private HelsimTransmitter helsimTransmitter = new HelsimTransmitter();

        public HelsimSorter(SortingCostCalculator costCalculator)
        {
            this.costCalculator = costCalculator;
        }

        public void Sort(SimulationStructure simulationStructure, int step)
        {
            helsimTransmitter.TransmitParticles(simulationStructure, step);
        }
    }
}