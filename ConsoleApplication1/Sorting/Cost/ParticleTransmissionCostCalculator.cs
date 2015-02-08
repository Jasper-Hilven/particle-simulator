using ParticleSimulation.CostCalculation.CostStorage;
using ParticleSimulation.Structuring;
using ParticleSimulation.Structuring.Sectioning;

namespace ParticleSimulation.Sorting.Cost
{
    public class ParticleTransmissionCostCalculator
    {
        private const double SendCost =  7.14077E-009;
        private const double ReceiveCost = 7.14077E-009;
        private readonly int particleReductionFactor;
        private readonly SortingCostReceiveParticle receivingCost = new SortingCostReceiveParticle();
        private readonly SortingCostSendParticle sendingCost = new SortingCostSendParticle();
        private readonly ICostStorage storage;

        public ParticleTransmissionCostCalculator(int particleReductionFactor, ICostStorage storage)
        {
            this.particleReductionFactor = particleReductionFactor;
            this.storage = storage;
        }

        public void AddCost(IParticleSection section, IParticleSection otherSection, int nBParticles,
            SimulationStructure structure, int step)
        {
            var core1 = section.Core;
            var core2 = otherSection.Core;
            if (core1 == core2)
            {
                return;
            }
            storage.AddCost(sendingCost, core1, step, nBParticles*particleReductionFactor*SendCost);
            storage.AddCost(receivingCost, core2, step, nBParticles*particleReductionFactor*ReceiveCost);
        }
    }
}