using System.Linq;
using ParticleSimulation.Sorting;
using ParticleSimulation.Sorting.Cost;
using ParticleSimulation.Structuring;
using ParticleSimulation.Structuring.Sectioning;

namespace ParticleSimulation.Tactics.MatrixSort
{
    public class MatrixSortTransmitter : IParticleTransmitter
    {
        // private ICostStorage costStorage;
        private readonly SortingCostCalculator costCalculator;

        public MatrixSortTransmitter(SortingCostCalculator calculator)
        {
            //   this.costStorage = costStorage;
            costCalculator = calculator;
        }

        public void TransmitParticles(SimulationStructure structure, int step)
        {
            foreach (
                var section in
                    structure.CoreGrid.GetCores().SelectMany(core => core.Sections.Cast<MatrixSortParticleSection>()))
            {
                Point3 p;
                IParticleSection otherParticleSection;
                section.Grid.SectionCoreMapping.TryGetValue(section, out p);
                if (section.Grid.SectionCoreMapping.TryGetKey(p + new Point3(1, 0, 0), out otherParticleSection))
                    SwapXBorderSection(section, ((MatrixSortParticleSection) otherParticleSection), step, structure);
                if (section.Grid.SectionCoreMapping.TryGetKey(p + new Point3(0, 1, 0), out otherParticleSection))
                    SwapYBorderSection(section, ((MatrixSortParticleSection) otherParticleSection), step, structure);
                if (section.Grid.SectionCoreMapping.TryGetKey(p + new Point3(0, 0, 1), out otherParticleSection))
                    SwapZBorderSection(section, ((MatrixSortParticleSection) otherParticleSection), step, structure);
            }
        }


        private void SwapXBorderSection(MatrixSortParticleSection lower, MatrixSortParticleSection upper, int step,
            SimulationStructure structure)
        {
            for (int i = 1; i < lower.ParticlesMatrix.GetLength(1); i++)
            {
                for (int j = 1; j < lower.ParticlesMatrix.GetLength(2); j++)
                {
                    Particle particle1 = lower.ParticlesMatrix[lower.ParticlesMatrix.GetLength(0) - 1, i, j];
                    Particle particle2 = upper.ParticlesMatrix[0, i, j];
                    if (particle1.Position.X <= particle2.Position.X)
                        continue;
                    lower.ParticlesMatrix[lower.ParticlesMatrix.GetLength(0) - 1, i, j] = particle2;
                    upper.ParticlesMatrix[0, i, j] = particle1;
                }
            }
            costCalculator.AddTransmissionCost(lower, upper,
                lower.ParticlesMatrix.GetLength(2)*lower.ParticlesMatrix.GetLength(1), structure, step);
        }


        private void SwapYBorderSection(MatrixSortParticleSection lower, MatrixSortParticleSection upper, int step,
            SimulationStructure structure)
        {
            for (int i = 0; i < lower.ParticlesMatrix.GetLength(0); i++)
            {
                for (int j = 1; j < lower.ParticlesMatrix.GetLength(2); j++)
                {
                    Particle particle1 = lower.ParticlesMatrix[i, lower.ParticlesMatrix.GetLength(1) - 1, j];
                    Particle particle2 = upper.ParticlesMatrix[i, 0, j];
                    if (particle1.Position.Y <= particle2.Position.Y)
                        continue;
                    lower.ParticlesMatrix[i, lower.ParticlesMatrix.GetLength(1) - 1, j] = particle2;
                    upper.ParticlesMatrix[i, 0, j] = particle1;
                }
            }
            costCalculator.AddTransmissionCost(lower, upper,
                lower.ParticlesMatrix.GetLength(2)*lower.ParticlesMatrix.GetLength(0), structure, step);
        }


        private void SwapZBorderSection(MatrixSortParticleSection lower, MatrixSortParticleSection upper, int step,
            SimulationStructure structure)
        {
            for (int i = 0; i < lower.ParticlesMatrix.GetLength(0); i++)
            {
                for (int j = 0; j < lower.ParticlesMatrix.GetLength(1); j++)
                {
                    Particle particle1 = upper.ParticlesMatrix[i, j, 0];
                    Particle particle2 = lower.ParticlesMatrix[i, j, lower.ParticlesMatrix.GetLength(2) - 1];
                    if (particle2.Position.Z <= particle1.Position.Z)
                        continue;
                    lower.ParticlesMatrix[i, j, lower.ParticlesMatrix.GetLength(2) - 1] = particle1;
                    upper.ParticlesMatrix[i, j, 0] = particle2;
                }
            }
            costCalculator.AddTransmissionCost(lower, upper,
                lower.ParticlesMatrix.GetLength(2)*lower.ParticlesMatrix.GetLength(1), structure, step);
        }
    }
}