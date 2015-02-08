using System;
using ParticleSimulation.CostCalculation.CostReviewStorage;
using ParticleSimulation.FieldCalculation;
using ParticleSimulation.FieldCalculation.Cost;
using ParticleSimulation.Initialization;
using ParticleSimulation.Moving;
using ParticleSimulation.Sorting;
using ParticleSimulation.Sorting.Cost;
using ParticleSimulation.Structuring;
using ParticleSimulation.Visualization.FinalVisualization;
using ParticleSimulation.Visualization.Initialization;
using ParticleSimulation.Visualization.StepVisualization;

namespace ParticleSimulation
{
    public class ParticleSimulator
    {
        private FieldCalculator fieldCalculator;
        private IFinalVisualizer finalVisualizer;
        private IParticleMover mover;
        private SimulationInitializer simulationInitializer;
        private ISorter sorter;
        private float stepSize;
        private IStepVisualizer stepVisualizer;
        private int steps;
        private IReviewCostStorage storage;
        private SimulationStructure structure;

        public void Simulate(InitializationParameters parameters)
        {
            ExtractParameters(parameters);
            for (var i = 0; i < steps; i++)
            {
                if (i%5 == 0)
                    Console.Out.WriteLine("\r\n\r\nSTARTING STEP: " + i + "\r\n\r\n");
                mover.MoveParticles(structure, stepSize, i);
                sorter.Sort(structure, i);
                fieldCalculator.CalculateField(structure, i);
                stepVisualizer.VisualizeStep(structure, i);
                
            }
            finalVisualizer.Visualize(steps - 1);
        }

        private void ExtractParameters(InitializationParameters parameters)
        {
            storage = parameters.CostStorage;
            var com = parameters.ComCostFactory.GetCalculator(storage,
                parameters.CellsPerSectionSingleDim, parameters.ParticleReductionFactor);
            fieldCalculator = new FieldCalculator(com, new ConstSolveCostCalculator(storage));
            simulationInitializer = new SimulationInitializer(new Random(0), parameters.CoreFactory,
                parameters.SectionFactory);
            var sortingCostCalculator = new SortingCostCalculator(parameters.ParticleReductionFactor, storage);
            sorter = parameters.SorterFactory.GetSorter(sortingCostCalculator);
            mover =
                parameters.ParticleMoverFactory.GetIParticleMover(new MovementCostCalculator(storage,
                    parameters.ParticleReductionFactor));
            stepSize = parameters.TimeStep;
            steps = parameters.TotalSteps;
            structure = simulationInitializer.InitializeSimulation(mover, parameters);
            var visualizationBuilder = new VisualizationBuilder(parameters.VisualizationParameters);
            stepVisualizer = visualizationBuilder.GetStepVisualizer(storage);
            finalVisualizer = visualizationBuilder.GetFinalVisualizer(storage);
        }
    }
}