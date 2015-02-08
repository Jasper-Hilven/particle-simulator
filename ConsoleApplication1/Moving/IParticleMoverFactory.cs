using System;
using ParticleSimulation.CostCalculation;
using ParticleSimulation.CostCalculation.CostStorage;

namespace ParticleSimulation.Moving
{
    public interface IParticleMoverFactory
    {
        IParticleMover GetIParticleMover(MovementCostCalculator costCalculator);

    }
}