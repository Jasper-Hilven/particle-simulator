using System;
using System.Linq;
using ParticleSimulation.Structuring;
using ParticleSimulation.Tactics.KMeans.KMeansSectioning;
using SharpDX;

namespace ParticleSimulation.Tactics.KMeans.KMeansSorting
{
    public class SingleDimDistanceTransmissionPreparer:ISectionTransmissionPreparer
    {
        private float GrowParameter;

        public SingleDimDistanceTransmissionPreparer(float growParameter)
        {
            GrowParameter = growParameter;
        }

        public void PutParticlesForTransmission(SimulationStructure simulationStructure)
        {
            foreach (KMeansSection section in simulationStructure.CoreGrid.GetCores().SelectMany(o => o.Sections))
            {

                for (var i = 0; i < section.CurrentParticles.Count; i++)
                {
                    var particlePosition = section.CurrentParticles[i].Position;
                    var sectionPosition = section.CurrentPosition;
                    var dDif = calculatedSingleDimDif(particlePosition, sectionPosition, section);
                    var bestDif = dDif.Max();
                    if (bestDif <= 0)
                        continue;
                    for (var j = 0; j < 6; j++)
                    {
                        if (bestDif != dDif[j])
                            continue;
                        section.ToTransmit[j].Add(section.CurrentParticles[i]);
                        section.CurrentParticles[i] = Particle.Null;
                        break;
                    }
                }
                //o => !o.IsNull
                section.CurrentParticles = section.CurrentParticles.Where(o => !o.IsNull).ToList();

            }
        }

        private float[] calculatedSingleDimDif(Vector3 particlePosition, Vector3 sectionPosition, KMeansSection section)
        {
            var dDif = new float[6];
            dDif[0] = Math.Abs(particlePosition.X - sectionPosition.X)*(1f + GrowParameter*section.CurrentParticles.Count()) -
                      Math.Abs(particlePosition.X - section.NeighbourPositions[0].X)*
                      (1f + GrowParameter*section.neighbourSizes[0]);
            dDif[1] = Math.Abs(particlePosition.X - sectionPosition.X)*(1f + GrowParameter*section.CurrentParticles.Count()) -
                      Math.Abs(particlePosition.X - section.NeighbourPositions[1].X)*
                      (1f + GrowParameter*section.neighbourSizes[1]);

            dDif[2] = Math.Abs(particlePosition.Y - sectionPosition.Y)*(1f + GrowParameter*section.CurrentParticles.Count()) -
                      Math.Abs(particlePosition.Y - section.NeighbourPositions[2].Y)*
                      (1f + GrowParameter*section.neighbourSizes[2]);
            dDif[3] = Math.Abs(particlePosition.Y - sectionPosition.Y)*(1f + GrowParameter*section.CurrentParticles.Count()) -
                      Math.Abs(particlePosition.Y - section.NeighbourPositions[3].Y)*
                      (1f + GrowParameter*section.neighbourSizes[3]);

            dDif[4] = Math.Abs(particlePosition.Z - sectionPosition.Z)*(1f + GrowParameter*section.CurrentParticles.Count()) -
                      Math.Abs(particlePosition.Z - section.NeighbourPositions[4].Z)*
                      (1f + GrowParameter*section.neighbourSizes[4]);
            dDif[5] = Math.Abs(particlePosition.Z - sectionPosition.Z)*(1f + GrowParameter*section.CurrentParticles.Count()) -
                      Math.Abs(particlePosition.Z - section.NeighbourPositions[5].Z)*
                      (1f + GrowParameter*section.neighbourSizes[5]);
            return dDif;
        }
    }
}