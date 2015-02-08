using ParticleSimulation.Structuring;

namespace ParticleSimulation.Tactics.MatrixSort
{
    public class MatrixSortCoreFactory:ICoreFactory
    {
        public ICore GetCore()
        {
            return new MatrixSortCore();
        }
    }
}