using System.Diagnostics;
using System.Linq;
using ParticleSimulation.Sorting;
using ParticleSimulation.Sorting.Cost;
using ParticleSimulation.Structuring;
using ParticleSimulation.Structuring.Sectioning;

namespace ParticleSimulation.Tactics.OuterList
{
    public class StrictBoundarySorter : ISorter
    {
        private readonly SortingCostCalculator sortCostCalculator;
        private readonly StrictBoundaryParticleTransmitter transmitter;
        public StrictBoundarySorter(SortingCostCalculator sortCostCalculator)
        {
            this.sortCostCalculator = sortCostCalculator;
            transmitter = new StrictBoundaryParticleTransmitter(sortCostCalculator);
        }
        
        public void Sort(SimulationStructure simulationStructure, int step)
        {
            foreach (var core in simulationStructure.CoreGrid.GetCores())
            {
                foreach (StrictBoundaryParticleSection section in core.Sections)
                {
                    SortInnerParticles(section, step,simulationStructure);
                    SortOuterParticles(section, step);
                }
            }
            transmitter.TransmitParticles(simulationStructure,step);
        }

        private void SortOuterParticles(IParticleSection particleSection, int step)
        {
            //DO NOTHING
        }


        private void SortInnerParticles(StrictBoundaryParticleSection coreParticleSection, int step, SimulationStructure simulationStructure)
        {
            for (var i = 0; i < coreParticleSection.CentralParticles.Count; i++)
            {
                var particle = coreParticleSection.CentralParticles[i];
                
                if (particle.Position.X < coreParticleSection.LowerBound.X)
                {
                    sortCostCalculator.AddComparisonCost(coreParticleSection.Core, 1, simulationStructure, step);
                    coreParticleSection.CentralParticles[i] = Particle.Null;
                    coreParticleSection.OuterParticleGroups[0].Add(particle);
                    coreParticleSection.CentralParticles[i] = Particle.Null;
                    continue;
                }
                
                if (particle.Position.X >= coreParticleSection.UpperBound.X)
                {
                    sortCostCalculator.AddComparisonCost(coreParticleSection.Core, 2, simulationStructure, step);
                    coreParticleSection.CentralParticles[i] = Particle.Null;
                    coreParticleSection.OuterParticleGroups[1].Add(particle);
                    coreParticleSection.CentralParticles[i] = Particle.Null;
                    continue;
                }
                if (particle.Position.Y < coreParticleSection.LowerBound.Y)
                {
                    sortCostCalculator.AddComparisonCost(coreParticleSection.Core, 3, simulationStructure, step);
                    coreParticleSection.CentralParticles[i] = Particle.Null;
                    coreParticleSection.OuterParticleGroups[2].Add(particle);
                    coreParticleSection.CentralParticles[i] = Particle.Null;
                    continue;
                }
                if (particle.Position.Y >= coreParticleSection.UpperBound.Y)
                {
                    sortCostCalculator.AddComparisonCost(coreParticleSection.Core, 4, simulationStructure, step);
                    coreParticleSection.CentralParticles[i] = Particle.Null;
                    coreParticleSection.OuterParticleGroups[3].Add(particle);
                    coreParticleSection.CentralParticles[i] = Particle.Null;
                    continue;
                }
                if (particle.Position.Z < coreParticleSection.LowerBound.Z)
                {
                    sortCostCalculator.AddComparisonCost(coreParticleSection.Core, 5, simulationStructure, step);
                    coreParticleSection.CentralParticles[i] = Particle.Null;
                    coreParticleSection.OuterParticleGroups[4].Add(particle);
                    coreParticleSection.CentralParticles[i] = Particle.Null;
                    continue;
                }
                sortCostCalculator.AddComparisonCost(coreParticleSection.Core, 6, simulationStructure, step);
                if (particle.Position.Z >= coreParticleSection.UpperBound.Z)
                {
                    coreParticleSection.CentralParticles[i] = Particle.Null;
                    coreParticleSection.OuterParticleGroups[5].Add(particle);
                    coreParticleSection.CentralParticles[i] = Particle.Null;
                }
            }
            coreParticleSection.CentralParticles = coreParticleSection.CentralParticles.Where(o=>!o.IsNull).ToList();
        }
    }
}