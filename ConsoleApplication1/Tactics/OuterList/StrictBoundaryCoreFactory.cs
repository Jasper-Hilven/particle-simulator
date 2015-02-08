using ParticleSimulation.Structuring;

namespace ParticleSimulation.Tactics.OuterList
{
    public class StrictBoundaryCoreFactory:ICoreFactory 
    {
        public ICore GetCore()
        {
            return new StrictBoundaryCore();
        }
    }
}