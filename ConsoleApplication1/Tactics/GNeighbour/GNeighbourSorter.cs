using ParticleSimulation.CostCalculation.CostStorage;
using ParticleSimulation.Sorting;
using ParticleSimulation.Sorting.Cost;
using ParticleSimulation.Structuring;
using System.Linq;
using ParticleSimulation.Tactics.KMeans.KMeansSectioning;
using ParticleSimulation.Tactics.KMeans.KMeansSorting;
using SharpDX;

namespace ParticleSimulation.Tactics.GNeighbour
{
    public class GNeighbourSorter:ISorter
    {
        private SortingCostCalculator costCalculator;
        private NeighbourSizeGetter neighbourSizeGetter;
        private NeighbourPositionGetter neighbourPositionGetter;
        private ISectionPositionUpdater positionUpdater;
        private NeighbourTransmitter nTransmitter;
        public GNeighbourSorter(SortingCostCalculator costCalculator, float sectionCenterFreedom)
        {
            neighbourPositionGetter = new NeighbourPositionGetter();
            neighbourSizeGetter = new NeighbourSizeGetter(1);
            positionUpdater = new GNeighbourSectionPositionUpdater(sectionCenterFreedom * sectionCenterFreedom);
            nTransmitter = new NeighbourTransmitter(costCalculator);
            this.costCalculator = costCalculator;
        }

        public void Sort(SimulationStructure simulationStructure, int step)
        {
            neighbourPositionGetter.GetOldPositionsNeighbours(simulationStructure);
            neighbourSizeGetter.GetOldSizesNeighbours(simulationStructure);
            positionUpdater.UpdateSectionPositions(simulationStructure);
            foreach (KMeansSection section in simulationStructure.CoreGrid.GetCores().SelectMany(o=>o.Sections))
            {
                for (var i = 0; i < section.CurrentParticles.Count; i++)
                {
                    var ownDistance = Vector3.DistanceSquared(section.CurrentParticles[i].Position, section.CurrentPosition);
                    var minDistance = ownDistance;
                    var bestDirection = -1;
                    for (var j = 0; j < 6; j++)
                    {
                        var localDistance = Vector3.DistanceSquared(section.CurrentParticles[i].Position, section.NeighbourPositions[j]);
                        if (!(localDistance < minDistance)) continue;
                        bestDirection = j;
                        minDistance = localDistance;
                    }
                    if (bestDirection == -1)
                        continue;
                    section.ToTransmit[bestDirection].Add(section.CurrentParticles[i]);
                    section.CurrentParticles[i] = Particle.Null;
                }
                section.CurrentParticles = section.CurrentParticles.Where(o => !o.IsNull).ToList();
            
            }
            nTransmitter.TransmitParticles(simulationStructure,step);
        }

        
    }
}