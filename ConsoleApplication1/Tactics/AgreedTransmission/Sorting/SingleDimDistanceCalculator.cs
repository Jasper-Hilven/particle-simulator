using System;
using ParticleSimulation.Sorting;
using SharpDX;

namespace ParticleSimulation.Tactics.AgreedTransmission.Sorting
{
    public class SingleDimDistanceCalculator
    {
        public float[] CalculatedSingleDimDif(Vector3 particlePosition, Vector3 sectionPosition,
                                              NeighbourTransferSection section)
        {
            var dDif = new float[6];
            dDif[0] = Math.Abs(particlePosition.X - sectionPosition.X) -
                      Math.Abs(particlePosition.X - section.NeighbourPositions[0].X);
            dDif[1] = Math.Abs(particlePosition.X - sectionPosition.X) -
                      Math.Abs(particlePosition.X - section.NeighbourPositions[1].X);

            dDif[2] = Math.Abs(particlePosition.Y - sectionPosition.Y) -
                      Math.Abs(particlePosition.Y - section.NeighbourPositions[2].Y);
            dDif[3] = Math.Abs(particlePosition.Y - sectionPosition.Y) -
                      Math.Abs(particlePosition.Y - section.NeighbourPositions[3].Y);

            dDif[4] = Math.Abs(particlePosition.Z - sectionPosition.Z) -
                      Math.Abs(particlePosition.Z - section.NeighbourPositions[4].Z);
            dDif[5] = Math.Abs(particlePosition.Z - sectionPosition.Z) -
                      Math.Abs(particlePosition.Z - section.NeighbourPositions[5].Z);
            return dDif;    
        }
    }
}