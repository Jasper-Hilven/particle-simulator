using ParticleSimulation.Initialization;
using ParticleSimulation.Structuring;
using ParticleSimulation.Structuring.Sectioning;
using SharpDX;

namespace ParticleSimulation.Tactics.Helsim
{
    public class HelsimSectionFactory:ISectionFactory
    {
        public IParticleSection GetSection(Vector3 lowerBound, Vector3 upperBound, ICore core, SectionGrid grid)
        {
            return new HelsimSection(grid,core){LowerBound = lowerBound,UpperBound = upperBound};
        }
    }
}