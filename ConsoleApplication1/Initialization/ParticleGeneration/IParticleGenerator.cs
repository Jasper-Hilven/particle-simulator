using ParticleSimulation.Structuring;

namespace ParticleSimulation.Initialization.ParticleGeneration
{
    public interface IParticleGenerator
    {
        void GenerateParticles(CoreGrid grid, int particlesPerSection);
    }
}