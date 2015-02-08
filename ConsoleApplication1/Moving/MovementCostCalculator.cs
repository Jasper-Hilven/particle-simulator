using System;
using System.Linq;
using ParticleSimulation.CostCalculation;
using ParticleSimulation.CostCalculation.CostStorage;
using ParticleSimulation.Structuring;

namespace ParticleSimulation.Moving
{
    public class MovementCostCalculator
    {
        private readonly ICostStorage storage;
        private readonly ICostType particleCost = ParticleMovementCost.GetParticleMovementCostType();
        private readonly int particleReductionFactor;

        public MovementCostCalculator(ICostStorage storage, int particleReductionFactor)
        {
            this.storage = storage;
            this.particleReductionFactor = particleReductionFactor;
        }

        private void CalculateParticleCost(SimulationStructure structure, int step)
        {
            const double movementCost = 1.1454431510266E-007;
            const double chargeDepositionCost = 0.000000182;
            const double noReduction = movementCost + chargeDepositionCost;
            //Console.Out.WriteLine(noReduction);
            var sumCost = noReduction * particleReductionFactor;
            foreach (var core in structure.CoreGrid.GetCores())
            {
                storage.AddCost(particleCost, core, step, core.Sections.Sum(o => o.Particles.Count() * sumCost));
            }
        }


        public void CalculateTotalCost(SimulationStructure structure, int step)
        {
            CalculateParticleCost(structure, step);

        }

    }
}