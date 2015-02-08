using ParticleSimulation.FieldCalculation.Cost;
using ParticleSimulation.Structuring;

namespace ParticleSimulation.FieldCalculation
{
    public class FieldCalculator
    {
        private readonly IFieldComCostCalculator comCostCalculator;
        private readonly ConstSolveCostCalculator constSolveCostCalculator;
        public FieldCalculator(IFieldComCostCalculator comCostCalculator, ConstSolveCostCalculator constSolveCostCalculator)
        {
            this.comCostCalculator = comCostCalculator;
            this.constSolveCostCalculator = constSolveCostCalculator;
        }

        public void CalculateField(SimulationStructure structure, int step)
        {   
            foreach (var core in structure.CoreGrid.GetCores())
            {
                foreach (var section in core.Sections)
                {
                    foreach (var particle in section.Particles) 
                        comCostCalculator.AddParticleCost(core, section.GetContainingParticle(particle.Position).Core, particle, step);
                }
                constSolveCostCalculator.AddCostForCore(core, step);
            }
            comCostCalculator.FlushCommunicationCosts(step);
        }
    }
}