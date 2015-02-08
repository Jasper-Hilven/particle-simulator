using ParticleSimulation.Structuring;

namespace ParticleSimulation.Tactics.AgreedTransmission
{
    public class FtCoreFactory:ICoreFactory
    {
        public ICore GetCore()
        {
            return new FtCore();
        }
    }
}