using ParticleSimulation.Initialization;
using ParticleSimulation.Structuring;
using ParticleSimulation.Structuring.Sectioning;
using SharpDX;

namespace ParticleSimulation.Tactics.OuterList
{
    public class StrictBoundarySectionFactory:ISectionFactory
    {
        public IParticleSection GetSection(Vector3 lowerBound, Vector3 upperBound, ICore core, SectionGrid sectionGrid)
        {
            return new StrictBoundaryParticleSection(sectionGrid,core){LowerBound = lowerBound,UpperBound = upperBound};
        }
    }
}