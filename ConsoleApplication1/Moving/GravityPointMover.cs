using System;
using ParticleSimulation.CostCalculation;
using ParticleSimulation.CostCalculation.CostStorage;
using ParticleSimulation.Structuring;
using ParticleSimulation.Structuring.Sectioning;
using SharpDX;
using System.Linq;

namespace ParticleSimulation.Moving
{
    public class GravityPointMover:IParticleMover
    {
        private ParticleBouncer bouncer;

        private float centerRemainTime;
        private Vector3 gravityPoint;
        private float swapTime;
        private Random random;
        private float maxSpeedSquared;
        private MovementCostCalculator costCalculator;

        public GravityPointMover(ParticleBouncer bouncer, float swapTime, Random random, float maxSpeed,MovementCostCalculator calculator)
        {
            throw new Exception("Outdated, takes no max speed into account");
            this.bouncer = bouncer;
            this.swapTime = swapTime;
            this.random = random;
            gravityPoint = new Vector3();
            maxSpeedSquared = maxSpeed*maxSpeed;
            costCalculator = calculator;
        }

        public void MoveParticles(SimulationStructure structure, float step, int simStep)
        {
            var dampingF =(float) (1/Math.Exp(step));

            foreach (var particleSection in structure.GetAllSections())
            {
                particleSection.ApplyToAllParticles(particle =>
                {
                    particle.Position += particle.Velocity * step;
                    particle.Velocity += step * (gravityPoint - particle.Position);
                    bouncer.BounceParticle(ref particle);
                    if (particle.Velocity.LengthSquared() > maxSpeedSquared)
                        particle.Velocity *= 0.9f;
                    return particle;
                });
            }
            centerRemainTime -= step;
            if(centerRemainTime < 0)
                UpdateGravityPoint(structure);
            costCalculator.CalculateTotalCost(structure, simStep);
        }

        private void UpdateGravityPoint(SimulationStructure  structure)
        {
            var sections = structure.CoreGrid.GetCores().SelectMany(c => c.Sections).ToList();
            var particleSection = sections[random.Next(sections.Count)];
            gravityPoint = particleSection.UpperBound / 2 + particleSection.LowerBound / 2;
            centerRemainTime += swapTime;
        }

        public void SetBoundarySection(IParticleSection overParticleSection)
        {
            bouncer.SetBoundarySection(overParticleSection);
        }
    }
    public class GravityPointMoverFactory:IParticleMoverFactory
    {
        private readonly float swapTime;
        private readonly Random random;
        private float maxSpeed;

        public GravityPointMoverFactory( Random random,float swapTime,float maxSpeed)
        {
            this.swapTime = swapTime;
            this.random = random;
            this.maxSpeed = maxSpeed;
        }

        public IParticleMover GetIParticleMover(MovementCostCalculator calculator)
        {
            return new GravityPointMover(new ParticleBouncer(), swapTime, random, maxSpeed,calculator);
        }
    }
}