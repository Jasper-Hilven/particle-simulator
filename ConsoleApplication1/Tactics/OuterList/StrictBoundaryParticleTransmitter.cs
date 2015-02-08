using System.Collections.Generic;
using System.Linq;
using ParticleSimulation.Sorting;
using ParticleSimulation.Sorting.Cost;
using ParticleSimulation.Structuring;
using ParticleSimulation.Structuring.Sectioning;

namespace ParticleSimulation.Tactics.OuterList
{
    public class StrictBoundaryParticleTransmitter : IParticleTransmitter
    {
        private readonly SortingCostCalculator costCalculator;

         public StrictBoundaryParticleTransmitter(SortingCostCalculator costCalculator)
        {
             this.costCalculator = costCalculator;
        }

        public void TransmitParticles(SimulationStructure structure, int step)
        {
            foreach (StrictBoundaryCore core in structure.CoreGrid.GetCores())
            {
                foreach (StrictBoundaryParticleSection section in core.Sections)
                {
                    Point3 myPosition;
                    section.Grid.SectionCoreMapping.TryGetValue(section,out myPosition);
                    
                                       TransferParticles(section, myPosition + new Point3(-1, 0, 0),
                                      section.OuterParticleGroups[0], step,structure);
                                       TransferParticles(section, myPosition + new Point3(1, 0, 0),
                                      section.OuterParticleGroups[1], step, structure);
                                       TransferParticles(section, myPosition + new Point3(0, -1, 0),
                                      section.OuterParticleGroups[2], step, structure);
                                       TransferParticles(section, myPosition + new Point3(0, 1, 0),
                                      section.OuterParticleGroups[3], step, structure);
                                       TransferParticles(section, myPosition + new Point3(0, 0, -1),
                                      section.OuterParticleGroups[4], step, structure);
                                       TransferParticles(section, myPosition + new Point3(0, 0, 1),
                                      section.OuterParticleGroups[5], step, structure);
                }
            }
        }
        private void TransferParticles(StrictBoundaryParticleSection currentParticleSection, Point3 nextPosition, IEnumerable<Particle> particles, int step, SimulationStructure structure)
        {
            if (!particles.Any())
                return;
            IParticleSection otherParticleSection;
            currentParticleSection.Grid.SectionCoreMapping.TryGetKey(nextPosition,out otherParticleSection);

            if (otherParticleSection == null)
                return;
            var cloneParticles = particles.ToList();
            foreach (var particle in cloneParticles)
            {
                currentParticleSection.RemoveParticle(particle);
                otherParticleSection.AddParticle(particle);
             }

            costCalculator.AddTransmissionCost(currentParticleSection,(StrictBoundaryParticleSection)otherParticleSection,cloneParticles.Count,structure,step);
        }
    }
}
