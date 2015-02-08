using System;
using System.Linq;
using ParticleSimulation.Structuring;
using Sharp3D.Math.Core;
using SharpDX;

namespace ParticleSimulation.Initialization.ParticleGeneration
{
    /// <summary>
    /// Equally divides all particles to all cores. 
    /// The particles will have a position that lays in a ball with a center and a radius. 
    /// The particles  will be equally divided over this ball. With a random speed.
    /// </summary>
    public class BallParticleGenerator : IParticleGenerator
    {

        private double ballRadius;
        private Random random;
        private Vector3 ballCenter;
        private float maxSpeed;

        public BallParticleGenerator(Vector3 ballCenter, double ballRadius, Random random, float maxSpeed)
        {
            this.ballCenter = ballCenter;
            this.ballRadius = ballRadius;
            this.random = random;
            this.maxSpeed = maxSpeed;
        }

        public void GenerateParticles(CoreGrid grid, int particlesPerSection)
        {
            foreach (var particleSection in grid.GetCores().SelectMany(core => core.Sections))
            {
                for (var i = 0; i < particlesPerSection; i++)
                {
                    var randomPositionWithinSphere = GetRandomPositionWithinSphere(ballCenter, ballRadius);
                    var velocity = randomPositionWithinSphere - ballCenter;
                    if (velocity.LengthSquared() != 0)
                    {
                        velocity.Normalize();
                        velocity *= maxSpeed*((float)random.NextDouble());
                    }
                    particleSection.AddParticle(new Particle() { Position = randomPositionWithinSphere, Velocity = velocity });
                }
            }
        }

        private Vector3 GetRandomPositionWithinSphere(Vector3 ballCenter, double ballRadius)
        {

            var retBall = new Vector3(0.5f - 1f * (float)random.NextDouble(), 0.5f - 1f * (float)random.NextDouble(), 0.5f - 1f * (float)random.NextDouble());
            if (retBall.Length() != 0d)
                retBall /= retBall.Length();
            retBall *= (float)(ballRadius*random.NextDouble());
            var randomPositionWithinSphere = retBall + ballCenter;
            return randomPositionWithinSphere;
        }
    }
}