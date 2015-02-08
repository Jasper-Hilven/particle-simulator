using ParticleSimulation.CostCalculation;
using ParticleSimulation.CostCalculation.CostStorage;
using ParticleSimulation.Sorting;
using ParticleSimulation.Sorting.Cost;
using ParticleSimulation.Structuring;
using ParticleSimulation.Tactics.KMeans.KMeansSectioning;

namespace ParticleSimulation.Tactics.KMeans.KMeansSorting
{
    public class KMeansSorter : ISorter
    {
        //private float GrowParameter { get; set; }
        private bool initialCalculated;
        private readonly ISectionPositionUpdater sectionPositionUpdater;
        private readonly ISectionTransmissionPreparer transmissionPreparer;
        private NeighbourSizeGetter neighbourSizeGetter;
        private NeighbourPositionGetter neighbourPositionGetter;
        private SortingCostCalculator calculator;
        private readonly IParticleTransmitter particleTransmitter;
        public KMeansSorter(SortingCostCalculator calculator, float growParameter, ISectionPositionUpdater positionUpdater, ISectionTransmissionPreparer transmissionPreparer, NeighbourSizeGetter sizeGetter, NeighbourPositionGetter positionGetter)
        {       
          //  GrowParameter = growParameter;
            sectionPositionUpdater = positionUpdater;
            this.transmissionPreparer = transmissionPreparer;
            neighbourPositionGetter = positionGetter;
            neighbourSizeGetter = sizeGetter;
            this.calculator = calculator;
            particleTransmitter = new NeighbourTransmitter(calculator);
        }

        public void Sort(SimulationStructure simulationStructure, int step)
        {

            if (!initialCalculated)
            {
                for (int i = 0; i < 10; i++)
                {
                    sectionPositionUpdater.UpdateSectionPositions(simulationStructure);
                    
                
                }
                System.Console.Out.WriteLine("Updated K Means");
                initialCalculated = true;
            }
            neighbourPositionGetter.GetOldPositionsNeighbours(simulationStructure);
            neighbourSizeGetter.GetOldSizesNeighbours(simulationStructure);
            sectionPositionUpdater.UpdateSectionPositions(simulationStructure);
            transmissionPreparer.PutParticlesForTransmission(simulationStructure);

            particleTransmitter.TransmitParticles(simulationStructure,step);
        }
    }
}