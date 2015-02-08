using System;
using System.Linq;
using ParticleSimulation.Structuring;
using ParticleSimulation.Tactics.KMeans.KMeansSectioning;
using SharpDX;

namespace ParticleSimulation.Tactics.KMeans.KMeansSorting
{
    public class OneSideSingleDimensionTransmissionPreparer : ISectionTransmissionPreparer
    {
        private readonly float growParameter;

        public OneSideSingleDimensionTransmissionPreparer(float growParameter)
        {
            this.growParameter = growParameter;
        }

        void ISectionTransmissionPreparer.PutParticlesForTransmission(SimulationStructure simulationStructure)
        {


            foreach (KMeansSection section in simulationStructure.CoreGrid.GetCores().SelectMany(o => o.Sections))
            {
                var currentPosition = section.CurrentPosition;
                var mySize = (section.CurrentParticles.Count + section.LastSize)/2;
                for (var i = 0; i < section.CurrentParticles.Count; i++)
                {
                    var currentParticle = section.CurrentParticles[i];
                    var particlePosition = currentParticle.Position;
                    int xNeighbour, yNeighbour, zNeighbour;
                    float xTransScore, yTransScore, zTransScore;
                    var xGood = CalculationParticleMovementSingleDimension(particlePosition.X, currentPosition.X,
                                                                           section, 0, mySize, currentParticle, i, section.NeighbourPositions[0].X,
                                                                           section.NeighbourPositions[0 + 1].X,  growParameter, out xTransScore,
                                                                           out xNeighbour);


                    var yGood =
                        CalculationParticleMovementSingleDimension(particlePosition.Y, currentPosition.Y, section, 2,
                                         mySize, currentParticle, i, section.NeighbourPositions[2].Y, section.NeighbourPositions[2 + 1].Y,
                                                                     growParameter, out yTransScore, out yNeighbour);

                    var zGood = CalculationParticleMovementSingleDimension(particlePosition.Z, currentPosition.Z, section, 4,
                        mySize, currentParticle, i, section.NeighbourPositions[4].Z, section.NeighbourPositions[4 + 1].Z, growParameter, out zTransScore, out zNeighbour);

                    var best = Math.Max(Math.Max(xTransScore, yTransScore), zTransScore);
                    //TODO PLEASE REFACTOR TO ARRAY of dim 3 before...
                    if ((!xGood) && (!yGood) && (!zGood))
                        continue;
                    section.CurrentParticles[i] = Particle.Null;
                    if (best == xTransScore && xGood)
                    {
                        section.ToTransmit[xNeighbour].Add(currentParticle);
                        continue;
                    }
                    if (best == yTransScore && yGood)
                    {
                        section.ToTransmit[yNeighbour].Add(currentParticle);
                        continue;
                    }
                    if (best == zTransScore && zGood)
                    {
                        section.ToTransmit[zNeighbour].Add(currentParticle);
                        continue;
                    }


                }
                section.CurrentParticles = section.CurrentParticles.Where(o => !o.IsNull).ToList();
            }
        }

        private bool CalculationParticleMovementSingleDimension(float particleX, float currentX, KMeansSection section,
                                                                int dimensionIndex, float mySize, Particle currentParticle,
                                                                int i, float neighbourX, float nextNeighbourX,float growFactor, out float transScore, out int toNeighbour)
        {
            if (particleX < currentX)
            {
                if ((particleX < neighbourX) ||
                    ((1f + growFactor*mySize) * (currentX - particleX) > (particleX - neighbourX) * (1f + growFactor * section.neighbourSizes[dimensionIndex])))
                {

                    transScore = (1f + growFactor*mySize)*(currentX - particleX) -
                                 (particleX - neighbourX)*(1f + growFactor*section.neighbourSizes[dimensionIndex]);
                    toNeighbour = dimensionIndex;
                    return true;
                }
            }
            else
            {
                if ((particleX > nextNeighbourX) ||
                    ((1f + growFactor *mySize )* (particleX - currentX) > (nextNeighbourX - particleX) * (1f + growFactor * section.neighbourSizes[dimensionIndex + 1])) ) 
                {
                    transScore = ((1f + growFactor * mySize) * (particleX - currentX) - (nextNeighbourX - particleX) * (1f + growFactor * section.neighbourSizes[dimensionIndex + 1]));
                    toNeighbour = dimensionIndex + 1;
                    return true;
                }
            }
            toNeighbour = -1;
            transScore = float.MinValue;
            return false;
        }
    }
}