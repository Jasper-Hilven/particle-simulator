using System.Linq;
using ParticleSimulation.Sorting;
using ParticleSimulation.Sorting.Cost;
using ParticleSimulation.Structuring;

namespace ParticleSimulation.Tactics.MatrixSort
{
    public class MatrixSorter : ISorter
    {
        private readonly SortingCostCalculator calculator;
        private readonly MatrixSortTransmitter transmitter;
        private readonly int timesSorting;
        private readonly int perTimeSorting;

        public MatrixSorter(SortingCostCalculator calculator, int timesSorting, int perTimeSorting)
        {
            this.calculator = calculator;
            this.timesSorting = timesSorting;
            this.perTimeSorting = perTimeSorting;
            transmitter = new MatrixSortTransmitter(calculator);
        }

        private int totalSwaps;

        public void Sort(SimulationStructure simulationStructure, int step)
        {
            if (step % perTimeSorting != 0)
                return;

            for (int time = 0; time < timesSorting; time++)
            {


                foreach (var core in simulationStructure.CoreGrid.GetCores())
                {
                    for (var i = 0; i < timesSorting; i++)
                    {
                        foreach (MatrixSortParticleSection section in core.Sections)
                        {
                            PartialSortX(section);
                            PartialSortY(section);
                            PartialSortZ(section);
                        }
                    }
                    var first = (MatrixSortParticleSection)core.Sections.First();
                    calculator.AddComparisonCost(core, core.Sections.Count() *
                                                       (3 * first.ParticlesMatrix.GetLength(0) *
                                                        first.ParticlesMatrix.GetLength(1) *
                                                        first.ParticlesMatrix.GetLength(2)
                                                        -
                                                        first.ParticlesMatrix.GetLength(0) *
                                                        first.ParticlesMatrix.GetLength(1)
                                                        -
                                                        first.ParticlesMatrix.GetLength(0) *
                                                        first.ParticlesMatrix.GetLength(2)
                                                        -
                                                        first.ParticlesMatrix.GetLength(2) *
                                                        first.ParticlesMatrix.GetLength(1)), simulationStructure, step);
                    calculator.AddSwapCost(core, totalSwaps, simulationStructure, step);
                    totalSwaps = 0;
                }
                transmitter.TransmitParticles(simulationStructure, step);
            }
        }

        private void PartialSortZ(MatrixSortParticleSection particleSection)
        {
            for (var i = 0; i < particleSection.ParticlesMatrix.GetLength(0); i++)
            {
                for (var j = 0; j < particleSection.ParticlesMatrix.GetLength(1); j++)
                {
                    for (var k = 0; k < particleSection.ParticlesMatrix.GetLength(2) - 1; k++)
                    {
                        if (particleSection.ParticlesMatrix[i, j, k].Position.Z >
                            particleSection.ParticlesMatrix[i, j, k + 1].Position.Z)
                        {
                            SwapParticles(particleSection, i, j, k, i, j, k + 1);
                        }
                        var otherk = particleSection.ParticlesMatrix.GetLength(2) - k - 2;

                        if (particleSection.ParticlesMatrix[i, j, otherk].Position.Z >
                            particleSection.ParticlesMatrix[i, j, otherk + 1].Position.Z)
                        {
                            SwapParticles(particleSection, i, j, otherk, i, j, otherk + 1);
                        }

                    }
                }
            }
        }

        private void SwapParticles(MatrixSortParticleSection particleSection, int x1, int y1, int z1, int x2, int y2,
            int z2)
        {
            var swap = particleSection.ParticlesMatrix[x1, y1, z1];
            particleSection.ParticlesMatrix[x1, y1, z1] = particleSection.ParticlesMatrix[x2, y2, z2];
            particleSection.ParticlesMatrix[x2, y2, z2] = swap;
            totalSwaps++;
        }

        private void PartialSortY(MatrixSortParticleSection particleSection)
        {
            for (var i = 0; i < particleSection.ParticlesMatrix.GetLength(0); i++)
            {
                for (var j = 0; j < particleSection.ParticlesMatrix.GetLength(1) - 1; j++)
                {
                    for (var k = 0; k < particleSection.ParticlesMatrix.GetLength(2); k++)
                    {
                        if (particleSection.ParticlesMatrix[i, j, k].Position.Y >
                            particleSection.ParticlesMatrix[i, j + 1, k].Position.Y)
                        {
                            SwapParticles(particleSection, i, j, k, i, j + 1, k);
                        }
                        var otherj = particleSection.ParticlesMatrix.GetLength(1) - j - 2;

                        if (particleSection.ParticlesMatrix[i, otherj, k].Position.Y >
                              particleSection.ParticlesMatrix[i, otherj + 1, k].Position.Y)
                        {
                            SwapParticles(particleSection, i, otherj, k, i, otherj + 1, k);
                        }

                        // throw new Exception("Edit to swap from A to B, B to A");
                    }
                }
            }
        }

        private void PartialSortX(MatrixSortParticleSection particleSection)
        {
            for (var i = 0; i < particleSection.ParticlesMatrix.GetLength(0) - 1; i++)
            {
                for (var j = 0; j < particleSection.ParticlesMatrix.GetLength(1); j++)
                {
                    for (var k = 0; k < particleSection.ParticlesMatrix.GetLength(2); k++)
                    {
                        if (particleSection.ParticlesMatrix[i, j, k].Position.X >
                            particleSection.ParticlesMatrix[i + 1, j, k].Position.X)
                        {
                            SwapParticles(particleSection, i + 1, j, k, i, j, k);
                        }
                        var otheri = particleSection.ParticlesMatrix.GetLength(0) - i - 2;
                        if (particleSection.ParticlesMatrix[otheri, j, k].Position.X >
                            particleSection.ParticlesMatrix[otheri + 1, j, k].Position.X)
                        {
                            SwapParticles(particleSection, otheri + 1, j, k, otheri, j, k);
                        }
                    }
                }
            }
        }
    }
    /*
    public class MatrixSwap : ICostType
    {
        public string GetCostName()
        {
            return "Matrix_Swap";
        }
    }

    public class MatrixComparison : ICostType
    {
        public string GetCostName()
        {
            return "Matrix_Comparison";
        }
    }*/
}