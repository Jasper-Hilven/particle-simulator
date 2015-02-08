using System;
using ParticleSimulation.Moving;
using ParticleSimulation.Structuring;
using ParticleSimulation.Structuring.Sectioning;

namespace ParticleSimulation.Initialization
{
    public class SimulationInitializer
    {
        private CoreSectionBuilder coreSectionBuilder = new CoreSectionBuilder();
        private ICoreFactory coreFactory;
        private ISectionFactory sectionFactory;

        public SimulationInitializer(Random seed,ICoreFactory coreFactory,ISectionFactory sectionFactory)
        {
            this.coreFactory = coreFactory;
            this.sectionFactory = sectionFactory;
        }

        public SimulationStructure InitializeSimulation(IParticleMover mover,InitializationParameters parameters)
        {
            CoreGrid cGrid;
            IParticleSection overParticleSection;
            coreSectionBuilder.CreateCoresAndSections(coreFactory,sectionFactory,out cGrid, out overParticleSection,
                new Point3(parameters.XCores, parameters.YCores, parameters.ZCores), parameters.SectionSize, new Point3(parameters.XSections, parameters.YSections, parameters.ZSections));
            mover.SetBoundarySection(overParticleSection);
            parameters.ParticleGenerator.GenerateParticles(cGrid, parameters.ParticlesPerCell * parameters.CellsPerSectionSingleDim * parameters.CellsPerSectionSingleDim * parameters.CellsPerSectionSingleDim);
            return new SimulationStructure() {CoreGrid = cGrid};    
        }
    }
}