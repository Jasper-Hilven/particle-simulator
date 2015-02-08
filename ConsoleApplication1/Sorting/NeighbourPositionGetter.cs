using System.Linq;
using ParticleSimulation.Structuring;
using ParticleSimulation.Structuring.Sectioning;
using SharpDX;

namespace ParticleSimulation.Sorting
{
    public class NeighbourPositionGetter
    {
        public void GetOldPositionsNeighbours(SimulationStructure simulationStructure)
        {
            foreach (var section in simulationStructure.CoreGrid.GetCores().SelectMany(core => core.Sections.Cast<NeighbourTransferSection>()))
            {
                Point3 ownPos;
                IParticleSection otherParticleSection;
                section.Grid.SectionCoreMapping.TryGetValue(section, out ownPos);
                if (section.Grid.SectionCoreMapping.TryGetKey(ownPos + new Point3(-1, 0, 0), out otherParticleSection))
                    section.NeighbourPositions[0] = ((NeighbourTransferSection)otherParticleSection).CurrentPosition;
                else
                    section.NeighbourPositions[0] = new Vector3(-100, -100, -100);
                if (section.Grid.SectionCoreMapping.TryGetKey(ownPos + new Point3(1, 0, 0), out otherParticleSection))
                    section.NeighbourPositions[1] = ((NeighbourTransferSection)otherParticleSection).CurrentPosition;
                else
                    section.NeighbourPositions[1] = new Vector3(100, 100, 100);
                if (section.Grid.SectionCoreMapping.TryGetKey(ownPos + new Point3(0, -1, 0), out otherParticleSection))
                    section.NeighbourPositions[2] = ((NeighbourTransferSection)otherParticleSection).CurrentPosition;
                else
                    section.NeighbourPositions[2] = new Vector3(-100, -100, -100);
                if (section.Grid.SectionCoreMapping.TryGetKey(ownPos + new Point3(0, 1, 0), out otherParticleSection))
                    section.NeighbourPositions[3] = ((NeighbourTransferSection)otherParticleSection).CurrentPosition;
                else
                    section.NeighbourPositions[3] = new Vector3(100, 100, 100);
                if (section.Grid.SectionCoreMapping.TryGetKey(ownPos + new Point3(0, 0, -1), out otherParticleSection))
                    section.NeighbourPositions[4] = ((NeighbourTransferSection)otherParticleSection).CurrentPosition;
                else
                    section.NeighbourPositions[4] = new Vector3(-100, -100, -100);
                if (section.Grid.SectionCoreMapping.TryGetKey(ownPos + new Point3(0, 0, 1), out otherParticleSection))
                    section.NeighbourPositions[5] = ((NeighbourTransferSection)otherParticleSection).CurrentPosition;
                else
                    section.NeighbourPositions[5] = new Vector3(100, 100, 100);
                //TODO add cost.
            }
        }
    }
}