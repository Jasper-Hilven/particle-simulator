using System;
using ParticleSimulation.Structuring;
using ParticleSimulation.Structuring.Sectioning;
using SharpDX;

namespace ParticleSimulation.Moving
{
    public class WaveParticleMover : IParticleMover
    {
        private readonly ParticleBouncer bouncer = new ParticleBouncer();
        private readonly MovementCostCalculator costCalculator;
        private float maxSpeed;

        public WaveParticleMover(float maxSpeed, MovementCostCalculator costCalculator)
        {
            this.costCalculator = costCalculator;
            this.maxSpeed = maxSpeed;
        }

        public void MoveParticles(SimulationStructure structure, float step, int simStep)
        {
            var f = ((float) Math.Sin((float) simStep/400));


            foreach (var allSection in structure.GetAllSections())
            {
                allSection.ApplyToAllParticles(particle =>
                                               {
                                                   particle.Velocity = new Vector3((particle.Velocity.X + f/5)*0.5f, 0,
                                                       0);
                                                   particle.Position += particle.Velocity*step;
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