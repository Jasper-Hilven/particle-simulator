using ParticleSimulation.Initialization;
using ParticleSimulation.Structuring;
using ParticleSimulation.Structuring.Sectioning;
using SharpDX;

namespace ParticleSimulation.Tactics.MatrixSort
{
    public class MatrixSortSectionFactory:ISectionFactory
    {
        private readonly int cellSize;
        public MatrixSortSectionFactory(int cellSize)
        {
            this.cellSize = cellSize;
        }

        public IParticleSection GetSection(Vector3 lowerBound, Vector3 upperBound, ICore core, SectionGrid grid)
        {
            return new MatrixSortParticleSection(core,grid,cellSize){LowerBound = lowerBound,UpperBound = upperBound};
        }

    }
}