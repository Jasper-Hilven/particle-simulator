using ParticleSimulation.Structuring;

namespace ParticleSimulation.Tactics.KMeans
{
    public class KMeansCoreFactory:ICoreFactory
    {
        public ICore GetCore()
        {
            return new KMeansCore();
        }
    }
}