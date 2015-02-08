using ParticleSimulation.Structuring;
using ParticleSimulation.Structuring.Sectioning;
using SharpDX;

namespace ParticleSimulation.Initialization
{
    public interface ISectionFactory
    {
        IParticleSection GetSection(Vector3 lowerBound, Vector3 upperBound,ICore core,SectionGrid grid);
    }
}