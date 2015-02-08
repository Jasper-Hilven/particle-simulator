using System;
using System.Linq;
using ParticleSimulation.Structuring;
using ParticleSimulation.Tactics.KMeans.KMeansSectioning;
using SharpDX;

namespace ParticleSimulation.Sorting
{
    public class BoundedSectionPositionUpdater : ISectionPositionUpdater
    {
        public void UpdateSectionPositions(SimulationStructure simulationStructure)
        {
            foreach (
                var section in
                    simulationStructure.CoreGrid.GetCores().SelectMany(core => core.Sections.Cast<NeighbourTransferSection>()))
            {
                SetAveragePosition(section);
            }

        }
        private void SetAveragePosition(NeighbourTransferSection section)
        {
            {
                if (!section.InitializedPosition)
                {
                    section.CurrentPosition = (section.LowerBound + section.UpperBound) / 2;
                    section.InitializedPosition = true;
                }
                if (!section.Particles.Any())
                {
                    section.CurrentPosition = (6 * section.CurrentPosition + section.LowerBound + section.UpperBound) / 8;
                    return;
                }
                var particleAverage = new Vector3();
                particleAverage = section.Particles.Aggregate(particleAverage, (current, particle) => current + particle.Position) / section.Particles.Count();
                var flexibility = 1 - section.LocationStifness;

                section.CurrentPosition = section.CurrentPosition * section.LocationInertia + (1 - section.LocationInertia) * (particleAverage * flexibility + section.LocationStifness * (section.LowerBound + section.UpperBound) / 2);
                var sectionCenter = (section.UpperBound + section.LowerBound) / 2;
                if ((section.CurrentPosition - sectionCenter).LengthSquared() > section.MaximumDifferenceRadiusSquared)
                {
                    var direction = (section.CurrentPosition - sectionCenter);
                    direction.Normalize();
                    section.CurrentPosition = sectionCenter +
                                                      direction *
                                                      ((float)
                                                       Math.Sqrt( section.MaximumDifferenceRadiusSquared));
                }
                if (section.CurrentPosition.X > 1)
                    Console.Out.WriteLine("ola");
                section.CurrentPosition = new Vector3(section.CurrentPosition.X, section.CurrentPosition.Y, section.CurrentPosition.Z);
            }


        }
    }
}