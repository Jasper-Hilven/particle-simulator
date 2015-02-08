using System;
using System.Linq;
using ParticleSimulation.Sorting;
using ParticleSimulation.Structuring;
using SharpDX;

namespace ParticleSimulation.Tactics.KMeans.KMeansSectioning
{
    public class SectionPositionUpdater : ISectionPositionUpdater
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
                if (section.Particles.Count() == 0)
                {
                    section.CurrentPosition = (6 * section.CurrentPosition + section.LowerBound + section.UpperBound) / 8;
                    return;
                }
                var particleAverage = new Vector3();
                particleAverage = section.Particles.Aggregate(particleAverage, (current, particle) => current + particle.Position) / section.Particles.Count();
                var flexibility = 1 - section.LocationStifness;

                section.CurrentPosition = section.CurrentPosition * section.LocationInertia + (1 - section.LocationInertia) * (particleAverage * flexibility + section.LocationStifness * (section.LowerBound + section.UpperBound) / 2);
                if (section.CurrentPosition.X > 1)
                    Console.Out.WriteLine("ola");

                section.CurrentPosition = new Vector3(section.CurrentPosition.X, (section.LowerBound.Y + section.UpperBound.Y) / 2, section.CurrentPosition.Z); ;

            }
        }



    }
}