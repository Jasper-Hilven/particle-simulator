using System;
using ParticleSimulation.Structuring;
using ParticleSimulation.Tactics.KMeans.KMeansSectioning;
using System.Linq;
using SharpDX;

namespace ParticleSimulation.Tactics.GNeighbour
{
    public class GNeighbourSectionPositionUpdater : ISectionPositionUpdater
    {
        private float DistanceSquared;
        public GNeighbourSectionPositionUpdater(float distanceSquared)
        {
            DistanceSquared = distanceSquared;
        }

        public void UpdateSectionPositions(SimulationStructure simulationStructure)
        {
            foreach (KMeansSection section in simulationStructure.CoreGrid.GetCores().SelectMany(o => o.Sections))
            {
                var newX = section.CurrentPosition.X;
                if ((float)section.CurrentParticles.Count + (float)section.neighbourSizes[0] > 0)
                    newX += (section.CurrentPosition.X - section.NeighbourPositions[0].X) *
                            ((float)section.CurrentParticles.Count - (float)section.neighbourSizes[0]) /
                            ((float)section.CurrentParticles.Count + (float)section.neighbourSizes[0]);

                if ((float)section.CurrentParticles.Count + (float)section.neighbourSizes[1] > 0)
                    newX += (section.CurrentPosition.X - section.NeighbourPositions[1].X) *
                            ((float)section.CurrentParticles.Count - (float)section.neighbourSizes[1]) /
                           ((float)section.CurrentParticles.Count + (float)section.neighbourSizes[1]);

                var newY = section.CurrentPosition.Y;
                if ((float)section.CurrentParticles.Count + (float)section.neighbourSizes[2] > 0)
                    newY +=
                        (section.CurrentPosition.Y - section.NeighbourPositions[2].Y) *
                        ((float)section.CurrentParticles.Count - (float)section.neighbourSizes[2]) /
                        ((float)section.CurrentParticles.Count + (float)section.neighbourSizes[2]);
                if ((float)section.CurrentParticles.Count + (float)section.neighbourSizes[3] > 0)
                    newY += (section.CurrentPosition.Y - section.NeighbourPositions[3].Y) *
                                ((float)section.CurrentParticles.Count - (float)section.neighbourSizes[3]) /
                               ((float)section.CurrentParticles.Count + (float)section.neighbourSizes[3]);

                var newZ = section.CurrentPosition.Z;
                if ((float)section.CurrentParticles.Count + (float)section.neighbourSizes[4] > 0)
                    newZ +=
                        (section.CurrentPosition.Z - section.NeighbourPositions[4].Z)*
                        ((float) section.CurrentParticles.Count - (float) section.neighbourSizes[4])/
                        ((float) section.CurrentParticles.Count + (float) section.neighbourSizes[4]);
                if ((float)section.CurrentParticles.Count + (float)section.neighbourSizes[5] > 0)
                    newZ += (section.CurrentPosition.Z - section.NeighbourPositions[5].Z) *
                                    ((float)section.CurrentParticles.Count - (float)section.neighbourSizes[5]) /
                                   ((float)section.CurrentParticles.Count + (float)section.neighbourSizes[5]);
                var newPos = new Vector3(newX, newY, newZ);
                correctToNeighbours(newPos,section.NeighbourPositions);
                newPos = 3*section.CurrentPosition/4 + newPos/4;
                var sectionMean = section.LowerBound / 2 + section.UpperBound / 2;
                
                if (Vector3.DistanceSquared(newPos, sectionMean) < DistanceSquared || Vector3.DistanceSquared(newPos, sectionMean) < 0.000001f)
                {
                    section.CurrentPosition = newPos; continue;
                }
                var direction = Vector3.Normalize(newPos - sectionMean);
                section.CurrentPosition = sectionMean + direction * ((float)Math.Sqrt(DistanceSquared));
            }
        }

        private void correctToNeighbours(Vector3 newPos, Vector3[] neighbourPositions)
        {
            newPos.X = Math.Min(neighbourPositions[1].X, Math.Max(neighbourPositions[0].X, newPos.X));
            newPos.Y = Math.Min(neighbourPositions[3].Y, Math.Max(neighbourPositions[2].Y, newPos.Y));
            newPos.Z = Math.Min(neighbourPositions[5].Z, Math.Max(neighbourPositions[4].Z, newPos.Z));
            
        }
    }
}