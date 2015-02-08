using System;
using ParticleSimulation.Initialization;
using ParticleSimulation.Initialization.ParticleGeneration;
using SharpDX;

namespace ParticleSimulation.Settings.HelperSettings
{
    public class ParticlePositionFactory
    {
        public void SetCentralizedPositions(InitializationParameters parameters)
        {
            parameters.ParticleGenerator = new CenterParticleGenerator(new Random(0));
        }

        public void SetSemiCentralizedPositions(InitializationParameters parameters)
        {
            throw new NotImplementedException();
        }

        public void SetDistributedPositions(InitializationParameters parameters)
        {
            parameters.ParticleGenerator = new LocalParticleGenerator(new Random(0));
        }

        public void SetBallDistribution(InitializationParameters parameters,Vector3 ballCenter,double ballRadius,float maxSpeed)
        {
            parameters.ParticleGenerator = new BallParticleGenerator(ballCenter, ballRadius, new Random(0),maxSpeed);
        }

        public void SetParticleRightCornerDistribution(InitializationParameters parameters)
        {
            parameters.ParticleGenerator = new RightDownCornerParticleGenerator();
        }
    }
}