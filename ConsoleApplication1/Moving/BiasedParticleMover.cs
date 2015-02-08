using System;
using ParticleSimulation.Structuring;
using ParticleSimulation.Structuring.Sectioning;
using SharpDX;

namespace ParticleSimulation.Moving
{
    public class BiasedParticleMover : IParticleMover
    {
        private readonly float acceleration;
        private readonly Vector3 accelerationBias;
        private readonly ParticleBouncer bouncer;
        private readonly MovementCostCalculator costCalculator;
        private readonly float maxSpeedSquared;
        private readonly Random randomizer;

        public BiasedParticleMover(Random randomizer, MovementCostCalculator calculator, float maxSpeed,
            Vector3 accelerationBias, float acceleration)
        {
            this.randomizer = randomizer;
            this.accelerationBias = accelerationBias;
            costCalculator = calculator;
            this.acceleration = acceleration;
            maxSpeedSquared = maxSpeed * maxSpeed;
            bouncer = new ParticleBouncer();
        }

        public void MoveParticles(SimulationStructure structure, float step, int simStep)
        {
            foreach (var allSection in structure.GetAllSections())
            {
                allSection.ApplyToAllParticles(
                    particle =>
                    {
                        
                        var randomAcceleration = step * acceleration *
                                                 (
                                                     new Vector3((float)(randomizer.NextDouble() - 0.5d),
                                                         (float)(randomizer.NextDouble() - 0.5d),
                                                         (float)(randomizer.NextDouble() - 0.5d)) +
                                                     accelerationBias);
                        particle.Velocity += randomAcceleration;
                        if (particle.Velocity.LengthSquared() > maxSpeedSquared)
                        {particle.Velocity.Normalize();
                        particle.Velocity = particle.Velocity * (float)Math.Sqrt(maxSpeedSquared);
                        }
                        particle.Position += particle.Velocity * step;
                        bouncer.BounceParticle(ref particle);
                        return particle;
                    });
            }
            costCalculator.CalculateTotalCost(structure, simStep);
        }


        public void SetBoundarySection(IParticleSection overParticleSection)
        {
            bouncer.SetBoundarySection(overParticleSection);
        }
    }
}