using ParticleSimulation.CostCalculation;

namespace ParticleSimulation.Moving
{
    public class ParticleMovementCost : ICostType
    {
        private static readonly ParticleMovementCost particleMovementCost = new ParticleMovementCost();

        public static ParticleMovementCost GetParticleMovementCostType()
        {
            return particleMovementCost;
        }

        private ParticleMovementCost()
        {
        }

        public string GetCostName()
        {
            return "Particle Mov & chargeDep";
        }
    }
}