using System.Linq;
using ParticleSimulation.Structuring;
using ParticleSimulation.Structuring.Sectioning;
using ParticleSimulation.Tactics.KMeans.KMeansSectioning;

namespace ParticleSimulation.Tactics.KMeans.KMeansSorting
{
    public class NeighbourSizeGetter
    {
        private readonly int inertia;

        public NeighbourSizeGetter(int inertia)
        {
            this.inertia = inertia;
        }

        public void GetOldSizesNeighbours(SimulationStructure simulationStructure)
        {
            foreach (var section in simulationStructure.CoreGrid.GetCores().SelectMany(core => core.Sections.Cast<KMeansSection>()))
            {
                Point3 ownPos;
                IParticleSection otherParticleSection;
                section.Grid.SectionCoreMapping.TryGetValue(section, out ownPos);
                var ownSize = section.CurrentParticles.Count;
                if (section.Grid.SectionCoreMapping.TryGetKey(ownPos + new Point3(-1, 0, 0), out otherParticleSection))
                    section.neighbourSizes[0] = (((KMeansSection)otherParticleSection).CurrentParticles.Count + section.neighbourSizes[0] * inertia) / (inertia + 1);
                else
                    section.neighbourSizes[0] = ownSize;
                if (section.Grid.SectionCoreMapping.TryGetKey(ownPos + new Point3(1, 0, 0), out otherParticleSection))
                    section.neighbourSizes[1] = (((KMeansSection)otherParticleSection).CurrentParticles.Count + section.neighbourSizes[1] * inertia) / (inertia + 1);
                else
                    section.neighbourSizes[1] = ownSize;
                if (section.Grid.SectionCoreMapping.TryGetKey(ownPos + new Point3(0, -1, 0), out otherParticleSection))
                    section.neighbourSizes[2] = (section.neighbourSizes[2] * inertia + ((KMeansSection)otherParticleSection).CurrentParticles.Count) / (inertia + 1);
                else
                    section.neighbourSizes[2] = ownSize;
                if (section.Grid.SectionCoreMapping.TryGetKey(ownPos + new Point3(0, 1, 0), out otherParticleSection))
                    section.neighbourSizes[3] = (section.neighbourSizes[3] * inertia + ((KMeansSection)otherParticleSection).CurrentParticles.Count) / (inertia + 1);
                else
                    section.neighbourSizes[3] = ownSize;
                if (section.Grid.SectionCoreMapping.TryGetKey(ownPos + new Point3(0, 0, -1), out otherParticleSection))
                    section.neighbourSizes[4] = (section.neighbourSizes[4] * inertia + ((KMeansSection)otherParticleSection).CurrentParticles.Count) / (inertia + 1);
                else
                    section.neighbourSizes[4] = ownSize;
                if (section.Grid.SectionCoreMapping.TryGetKey(ownPos + new Point3(0, 0, 1), out otherParticleSection))
                    section.neighbourSizes[5] = (section.neighbourSizes[5] * inertia + ((KMeansSection)otherParticleSection).CurrentParticles.Count) / (inertia + 1);
                else
                    section.neighbourSizes[5] = ownSize;
                //TODO add cost.
            }
        }
    }
}