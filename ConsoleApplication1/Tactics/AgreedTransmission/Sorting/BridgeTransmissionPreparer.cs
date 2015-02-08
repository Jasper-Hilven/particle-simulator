using System.Linq;
using ParticleSimulation.Structuring;
using ParticleSimulation.Structuring.Sectioning;
using ParticleSimulation.Tactics.KMeans.KMeansSectioning;
using ParticleSimulation.Tactics.KMeans.KMeansSorting;
using SharpDX;

namespace ParticleSimulation.Tactics.AgreedTransmission.Sorting
{
    public class BridgeTransmissionPreparer : ISectionTransmissionPreparer
    {
        private SingleDimDistanceCalculator distanceCalculator = new SingleDimDistanceCalculator();
        public void PutParticlesForTransmission(SimulationStructure simulationStructure)
        {

            foreach (FtSection section in simulationStructure.CoreGrid.GetCores().SelectMany(o => o.Sections))
            {
                section.NewOwnChanges = new int[6]; //Don't want to change next time
                for (var i = 0; i < section.CurrentParticles.Count; i++)
                {
                    var bestDistance = 0f;
                    var bestDirection = -1;
                    var distances = distanceCalculator.CalculatedSingleDimDif(section.CurrentParticles[i].Position,
                                                               section.CurrentPosition, section);
                    for (var j = 0; j < 6; j++)
                    {
                        var localDistance = distances[j];
                        if ((localDistance < bestDistance)) continue;
                        bestDirection = j;
                        bestDistance = localDistance;
                    }
                    if (bestDirection == -1)
                        continue;
                    if (section.NeighbourCanReceive[bestDirection] > 0)
                    {
                        section.NeighbourCanReceive[bestDirection]--;
                        section.ToTransmit[bestDirection].Add(section.CurrentParticles[i]);
                        section.CurrentParticles[i] = Particle.Null;
                    }
                    else
                    {
                       // System.Console.WriteLine("Full");
                    }
                    section.NewOwnChanges[bestDirection] += 1; // want a little bit more for change
                }
                section.CurrentParticles = section.CurrentParticles.Where(o => !o.IsNull).ToList();
            }
        }
    }

    public class BridgeTransmissionRandomPreparer
    {
        private SingleDimDistanceCalculator distanceCalculator = new SingleDimDistanceCalculator();
        public void PutParticlesForTransmission(SimulationStructure simulationStructure)
        {
            //TODO TRY VIA RANDOM
            foreach (FtSection section in simulationStructure.CoreGrid.GetCores().SelectMany(o => o.Sections))
            {
                section.NewOwnChanges = new int[6]; //Don't want to change next time
                for (var i = 0; i < section.CurrentParticles.Count; i++)
                {
                    var bestDistance = 0f;
                    var bestDirection = -1;
                    var distances = distanceCalculator.CalculatedSingleDimDif(section.CurrentParticles[i].Position, section.CurrentPosition, section);
                    for (var j = 0; j < 6; j++)
                    {
                        var localDistance = distances[j];
                        if (localDistance < bestDistance) continue;
                        bestDirection = j;
                        bestDistance = localDistance;
                    }
                    if (bestDirection == -1)
                        continue;
                    if (section.NeighbourCanReceive[bestDirection] > 0)
                    {
                        section.NeighbourCanReceive[bestDirection]--;
                        section.ToTransmit[bestDirection].Add(section.CurrentParticles[i]);
                        section.CurrentParticles[i] = Particle.Null;
                    }
                    section.NewOwnChanges[bestDirection] += 1; // want a little bit more for change
                }
                section.CurrentParticles = section.CurrentParticles.Where(o => !o.IsNull).ToList();
            }
        }


    
    }
}