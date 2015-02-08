using ParticleSimulation.Structuring;
using ParticleSimulation.Structuring.Sectioning;
using ParticleSimulation.Tactics.Helsim;
using ParticleSimulation.Tactics.KMeans;

namespace ParticleSimulation.Tactics.GNeighbour
{
    public class GNeighbourCoreFactory:ICoreFactory
    {
        public ICore GetCore()
        {
            return new KMeansCore();
        }
    }
}