using System;
using ParticleSimulation.CostCalculation;
using ParticleSimulation.CostCalculation.CostStorage;
using SharpDX;

namespace ParticleSimulation.Moving
{
    public class BiasedParticleMoverFactory : IParticleMoverFactory
    {
        private Random randomizer;
        private float maxSpeed;
        private Vector3 biasVector;
        private float acceleration;
        public BiasedParticleMoverFactory(Random randomizer, float maxSpeed, Vector3 biasVector,float acceleration)
        {
            this.randomizer = randomizer;
            this.maxSpeed = maxSpeed;
            this.biasVector = biasVector;
            this.acceleration = acceleration;
        }

        public IParticleMover GetIParticleMover(MovementCostCalculator calculator)
        {
            return new BiasedParticleMover(randomizer, calculator,maxSpeed, biasVector, acceleration);
        }
    }
}