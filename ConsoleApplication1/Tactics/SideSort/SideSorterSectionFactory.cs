using ParticleSimulation.Initialization;
using ParticleSimulation.Structuring;
using ParticleSimulation.Structuring.Sectioning;
using SharpDX;

namespace ParticleSimulation.Tactics.SideSort
{
    public class SideSorterSectionFactory:ISectionFactory
    {
        private int initialElementsPerSide;

        public SideSorterSectionFactory(int initialElementsPerSide)
        {
            this.initialElementsPerSide = initialElementsPerSide;
        }

        public IParticleSection GetSection(Vector3 lowerBound, Vector3 upperBound, ICore core, SectionGrid grid)
        {
            return new ParticleSection(grid,core,initialElementsPerSide){LowerBound = lowerBound,UpperBound = upperBound};
        }
    }
}