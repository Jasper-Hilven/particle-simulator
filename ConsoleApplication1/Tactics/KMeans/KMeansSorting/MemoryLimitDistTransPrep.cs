using System.Collections.Generic;
using System.Linq;
using ParticleSimulation.Structuring;
using ParticleSimulation.Tactics.KMeans.KMeansSectioning;
using SharpDX;

namespace ParticleSimulation.Tactics.KMeans.KMeansSorting
{
    public class MemoryLimitDistTransPrep : ISectionTransmissionPreparer
    {
        private readonly float GrowParameter;

        //Soft limit of number of particles that a core should have
        private readonly int maxParticles = 200;
        public MemoryLimitDistTransPrep(float growParameter)
        {
            GrowParameter = growParameter;
        }

        public void PutParticlesForTransmission(SimulationStructure simulationStructure)
        {
            foreach (KMeansSection section in simulationStructure.CoreGrid.GetCores().SelectMany(o => o.Sections))
            {
                PrepareSectionTransmission(section);
            }
        }

        private void PrepareSectionTransmission(KMeansSection section)
        {
            var jset = GetOpenNeighbours(section);
            //First keep the normal algorithm
            var i = 0;
            var kept = 0;
            while (i < section.CurrentParticles.Count && kept < maxParticles)
            {
                kept += 1;
                var bestDirection = BestDirection(section, i, jset,false);
                if (bestDirection == -1)
                {
                    i++; continue;
                }
                kept -= 1;
                section.ToTransmit[bestDirection].Add(section.CurrentParticles[i]);
                section.CurrentParticles[i] = Particle.Null;
                i++;
            }
            //Until we have the maximum limit of particles. Now give all rest to neighbours.
            while (i < section.CurrentParticles.Count)
            {
                kept += 1;
                var bestDirection = BestDirection(section, i, jset,true);
                if (bestDirection == -1)
                {
                    i++; continue;
                }
                kept -= 1;
                section.ToTransmit[bestDirection].Add(section.CurrentParticles[i]);
                section.CurrentParticles[i] = Particle.Null;
                i++;
            }


            section.CurrentParticles = section.CurrentParticles.Where(o => !o.IsNull).ToList();
        }

        private bool[] GetOpenNeighbours(KMeansSection section)
        {
            var jset = new bool[6];
            for (var j = 0; j < 6; j++)
            {
                if (section.neighbourSizes[j] < maxParticles || section.neighbourSizes[j] < 2 * section.CurrentParticles.Count)
                    jset[j] = true;
            }
            return jset;
        }

        private int BestDirection(KMeansSection section, int i, bool[] jset,bool ownHigh)
        {
            var ownDistance = Vector3.DistanceSquared(section.CurrentParticles[i].Position, section.CurrentPosition) *
                              (1f + GrowParameter * section.CurrentParticles.Count);
            
            var minDistance = ownDistance;
            if(ownHigh)
            {
                minDistance = 100;
            }
            var bestDirection = -1;
            for (var j = 0; j < 6; j++)
            {
                if (!jset[j])
                    continue;
                var localDistance =
                    Vector3.DistanceSquared(section.CurrentParticles[i].Position, section.NeighbourPositions[j]) *
                    (1f + GrowParameter * section.neighbourSizes[j]);
                if (!(localDistance < minDistance)) continue;
                bestDirection = j;
                minDistance = localDistance;
            }
            return bestDirection;
        }
    }
}