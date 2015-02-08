using ParticleSimulation.Structuring;

namespace ParticleSimulation.FieldCalculation.Cost
{
    public interface IFieldComCostCalculator
    {
        void AddParticleCost(ICore sectionCore, ICore homeCore, Particle particle, int step);
        void FlushCommunicationCosts(int step);
    }
}