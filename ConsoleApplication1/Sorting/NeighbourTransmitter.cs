using System.Collections.Generic;
using System.Linq;
using ParticleSimulation.CostCalculation.CostStorage;
using ParticleSimulation.Sorting.Cost;
using ParticleSimulation.Structuring;
using ParticleSimulation.Structuring.Sectioning;

namespace ParticleSimulation.Sorting
{
    public class NeighbourTransmitter : IParticleTransmitter
    {

        private readonly SortingCostCalculator particleTransmissionCostCalculator;
        public NeighbourTransmitter(SortingCostCalculator costCalculator)
        {
            this.particleTransmissionCostCalculator = costCalculator;
        }

        public void TransmitParticles(SimulationStructure structure, int step)
        {
            foreach (var core in structure.CoreGrid.GetCores())
            {
                foreach (NeighbourTransferSection section in core.Sections)
                {
                    if (section.ToTransmit[0].Any())
                        TransferParticles(section, 0, section.Grid, new Point3(-1, 0, 0),structure,step);
                    if (section.ToTransmit[1].Any())
                        TransferParticles(section, 1, section.Grid, new Point3(1, 0, 0), structure, step);
                    if (section.ToTransmit[2].Any())
                        TransferParticles(section, 2, section.Grid, new Point3(0, -1, 0), structure, step);
                    if (section.ToTransmit[3].Any())
                        TransferParticles(section, 3, section.Grid, new Point3(0, 1, 0), structure, step);
                    if (section.ToTransmit[4].Any())
                        TransferParticles(section, 4, section.Grid, new Point3(0, 0, -1), structure, step);
                    if (section.ToTransmit[5].Any())
                        TransferParticles(section, 5, section.Grid, new Point3(0, 0, 1), structure, step);
                }
            }
        }

        private void TransferParticles(NeighbourTransferSection section, int i, SectionGrid grid, Point3 point3,SimulationStructure structure, int step)
        {
            if (!section.ToTransmit[i].Any())
                return;
            IParticleSection outParticleSection;
            Point3 ownPosition;
            grid.SectionCoreMapping.TryGetValue(section, out ownPosition);
            if (!grid.SectionCoreMapping.TryGetKey(ownPosition + point3, out outParticleSection))
                return;
            particleTransmissionCostCalculator.AddTransmissionCost(section, (NeighbourTransferSection)outParticleSection, section.ToTransmit[i].Count, structure, step);
            ((NeighbourTransferSection)outParticleSection).CurrentParticles.AddRange(section.ToTransmit[i]);
            section.ToTransmit[i] = new List<Particle>();
            
        
        
        }
    }
}