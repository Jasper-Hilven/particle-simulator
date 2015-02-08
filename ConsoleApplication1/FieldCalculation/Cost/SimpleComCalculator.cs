using ParticleSimulation.CostCalculation;
using ParticleSimulation.CostCalculation.CostStorage;
using ParticleSimulation.FieldCalculation.Cost.Types;
using ParticleSimulation.Structuring;
using ParticleSimulation.Structuring.Sectioning;

namespace ParticleSimulation.FieldCalculation.Cost
{
    /// <summary>
    /// Does not yet take cohesion into account.
    /// </summary>
    public class SimpleComCalculator : IFieldComCostCalculator
    {
       private ICostType particlecorerole = ParticleWPCCoresHomeCoreRole.GetParticleWpcCoresSend();
        private ICostType sectioncorerole = ParticleWpcCoresSectionCoreRole.GetParticleWpcCoresReceiveCost();
        private readonly ICostStorage storage;
        private readonly int particleReductionFactor;
        const double FieldAggCost = 2.87807210286458E-007;
        const double ChargeAggCost = 0.000000678;
        private const double CohesionCorrectionFactor = 0.000047;
        public SimpleComCalculator(ICostStorage storage, int particleReductionFactor)
        {
            this.storage = storage;
            this.particleReductionFactor = particleReductionFactor;
        }

        public void AddParticleCost(ICore sectionCore, ICore homeCore, Particle particle, int step)
        {
            if (homeCore == sectionCore)
                return;

            var cost = (FieldAggCost + ChargeAggCost) * particleReductionFactor * CohesionCorrectionFactor;
            storage.AddCost(sectioncorerole, sectionCore, step, cost);
            storage.AddCost(sectioncorerole, homeCore, step, cost);
        }

        public void FlushCommunicationCosts(int step)
        {
            //DO NOTHING :)
        }

    }
}