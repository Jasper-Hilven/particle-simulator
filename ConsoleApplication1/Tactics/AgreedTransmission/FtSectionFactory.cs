using ParticleSimulation.Initialization;
using ParticleSimulation.Structuring;
using ParticleSimulation.Structuring.Sectioning;
using SharpDX;

namespace ParticleSimulation.Tactics.AgreedTransmission
{
    public class FtSectionFactory:ISectionFactory
    {
        private float maxDifRadSquared;
        private int maxParticles;

        public FtSectionFactory(float maxDifRadSquared, int maxParticles)
        {
            this.maxDifRadSquared = maxDifRadSquared;
            this.maxParticles = maxParticles;
        }

        public IParticleSection GetSection(Vector3 lowerBound, Vector3 upperBound, ICore core, SectionGrid grid)
        {
            return new FtSection(grid,core){LowerBound = lowerBound,UpperBound = upperBound,MaximumDifferenceRadiusSquared = maxDifRadSquared,MaximumParticles = maxParticles};
        }
    }
}