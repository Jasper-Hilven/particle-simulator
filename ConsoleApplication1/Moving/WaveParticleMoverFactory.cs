using System;
using ParticleSimulation.CostCalculation;
using ParticleSimulation.CostCalculation.CostStorage;

namespace ParticleSimulation.Moving
{
    public class WaveParticleMoverFactory:IParticleMoverFactory
    {
        private Random random;
        private float maxSpeed;

        public WaveParticleMoverFactory(Random random, float maxSpeed)
        {
            this.random = random;
            this.maxSpeed = maxSpeed;
        }

        public IParticleMover GetIParticleMover(MovementCostCalculator calculator)
        {
            return new WaveParticleMover(maxSpeed,calculator);
        }
    }
}