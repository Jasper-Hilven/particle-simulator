using ParticleSimulation.Structuring;

namespace ParticleSimulation.Tactics.SideSort
{
    public class SideSortCoreFactory:ICoreFactory
    {
        public ICore GetCore()
        {
            return new SideSortCore();
        }
    }
}