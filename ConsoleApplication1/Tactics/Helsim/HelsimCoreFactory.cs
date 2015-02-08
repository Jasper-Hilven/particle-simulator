using ParticleSimulation.Structuring;

namespace ParticleSimulation.Tactics.Helsim
{
    public class HelsimCoreFactory:ICoreFactory
    {
        public ICore GetCore()
        {
            return new HelsimCore();
        }
    }
}