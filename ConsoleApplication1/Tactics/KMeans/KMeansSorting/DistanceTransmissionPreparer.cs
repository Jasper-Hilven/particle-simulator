using System.Linq;
using ParticleSimulation.Structuring;
using ParticleSimulation.Tactics.KMeans.KMeansSectioning;
using SharpDX;

namespace ParticleSimulation.Tactics.KMeans.KMeansSorting
{
    public class DistanceTransmissionPreparer : ISectionTransmissionPreparer
    {
        private float GrowParameter;

        public DistanceTransmissionPreparer(float growParameter)
        {
            GrowParameter = growParameter;
        }

        public void PutParticlesForTransmission(SimulationStructure simulationStructure)
        {
            foreach (KMeansSection section in simulationStructure.CoreGrid.GetCores().SelectMany(o => o.Sections))
            {
                for (var i = 0; i < section.CurrentParticles.Count; i++)
                {
                    var ownDistance = Vector3.DistanceSquared(section.CurrentParticles[i].Position, section.CurrentPosition) * (1f + GrowParameter * section.CurrentParticles.Count);
                    var minDistance = ownDistance;
                    var bestDirection = -1;
                    for (var j = 0; j < 6; j++)
                    {
                        var localDistance = Vector3.DistanceSquared(section.CurrentParticles[i].Position, section.NeighbourPositions[j]) * (1f + GrowParameter * section.neighbourSizes[j]);
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
        }
    }
}