using System;
using ParticleSimulation.CostCalculation;
using ParticleSimulation.CostCalculation.CostReviewStorage;
using ParticleSimulation.CostCalculation.CostStorage;
using ParticleSimulation.FieldCalculation.Cost;
using ParticleSimulation.Initialization.ParticleGeneration;
using ParticleSimulation.Moving;
using ParticleSimulation.Structuring;
using ParticleSimulation.Visualization;
using ParticleSimulation.Visualization.Initialization;
using SharpDX;

namespace ParticleSimulation.Initialization
{
    public class InitializationParameters
    {
        public InitializationParameters()
        {
        }

        //SIZE OF SIMULATION
        public int XCores { get; set; }
        public int YCores { get; set; }
        public int ZCores { get; set; }
        public Vector3 SectionSize { get; set; }
        public int ParticlesPerCell { get; set; }
        public int ParticleReductionFactor { get; set; }
        public int XSections { get; set; }
        public int YSections { get; set; }
        public int ZSections { get; set; }
        public int TotalSteps { get; set; }
        public int CellsPerSectionSingleDim { get; set;}
        
        //INITIALIZATION
        public IParticleGenerator ParticleGenerator { get; set; }
        public Random Randi { get; set; }
        
        //TACTIC 
        public ICoreFactory CoreFactory { get; set; }
        public ISectionFactory SectionFactory { get; set; }
        public ISorterFactory SorterFactory { get; set; }

        //MOVEMENT
        public IParticleMoverFactory ParticleMoverFactory { get; set; }
        public float TimeStep { get; set;}

        //COSTSTORAGE
        public IReviewCostStorage CostStorage { get; set; }

        //FIELDCOMPUTATIONCOST
        public IComCostCalculatorFactory ComCostFactory { get; set; }
        
        //VISUALIZATION 
        public VisualizationParameters VisualizationParameters { get; set; }
    
        
    }
}