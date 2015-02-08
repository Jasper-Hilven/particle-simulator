using ParticleSimulation.CostCalculation;
using ParticleSimulation.CostCalculation.CostStorage;
using ParticleSimulation.Structuring;
using ParticleSimulation.Structuring.Sectioning;

namespace ParticleSimulation.Sorting.Cost
{
    public class SortingCostCalculator
    {
        private readonly ParticleTransmissionCostCalculator ptcostCalculator;
        private readonly ICostStorage costStorage;
        private ICostType swapCostType = new SortingCostParticleSwap();
        private double swapCostValueInTime = 4.308386E-09;
        private double compareCostValueInTime = 1.131344E-09 ;
        private ICostType comparisonCostType = new SortingCostParticleComparison();

        public SortingCostCalculator(int particleReductionFactor, ICostStorage costStorage)
        {
            this.costStorage = costStorage;
            ptcostCalculator = new ParticleTransmissionCostCalculator(particleReductionFactor, costStorage);
        }

        /*public void AddCalculationCost(ICostType compareCost, ICore outParticleSection, int step, double cost)
        {
            costStorage.AddCost(compareCost,outParticleSection,step,cost);
        }
        */

        public void AddSwapCost(ICore core, int nbParticles, SimulationStructure structure,
            int step)
        {
            costStorage.AddCost(swapCostType,core,step,nbParticles*swapCostValueInTime);
        }

        public void AddComparisonCost(ICore core, int nbParticles, SimulationStructure structure,
            int step)
        {
            costStorage.AddCost(comparisonCostType,core,step,nbParticles*compareCostValueInTime);

        }
        
        public void AddTransmissionCost(IParticleSection fromSection, IParticleSection toSection, int nbParticles,
            SimulationStructure structure, int step)
        {
            ptcostCalculator.AddCost(fromSection, toSection, nbParticles, structure, step);
        }
    }
}