using System;
using System.Drawing;
using System.Linq;
using ParticleSimulation.CostCalculation.CostReview;
using ParticleSimulation.CostCalculation.CostStorage;
using ParticleSimulation.FieldCalculation.Cost;
using ParticleSimulation.Initialization;
using ParticleSimulation.Initialization.ParticleGeneration;
using ParticleSimulation.Moving;
using SharpDX;

namespace ParticleSimulation.Settings.HelperSettings
{
    public class BasicSettingsFactory
    {
        public InitializationParameters InitializationParameters(int coreSize)
        {
            var retPar = new InitializationParameters();

            SetBasicSettings(retPar);
            retPar.TotalSteps = 800;
            retPar.XCores = coreSize;
            retPar.ParticleReductionFactor = 1;
            retPar.ZCores = coreSize;
            retPar.VisualizationParameters.SnapTime = 2;
            
            retPar.ParticleGenerator = new LocalParticleGenerator(new Random(0));
            retPar.VisualizationParameters.CostName = "UninitializedCostName";
            retPar.VisualizationParameters.ParticleVisualizationSize = new Point(400, 400);
            return retPar;
        }

        public void SetSizeSettings(InitializationParameters parameters)
        {
            parameters.ParticlesPerCell = 216;
            parameters.SectionSize = new Vector3(1f, 1f, 1f);
            parameters.XCores = 5;
            parameters.YCores = 1;
            parameters.ZCores = 5;
            parameters.XSections = 1;
            parameters.YSections = 1;
            parameters.ZSections = 1;
            parameters.CellsPerSectionSingleDim = 16;
        }

        public void SetVisualizationSettings(InitializationParameters parameters)
        {
            parameters.VisualizationParameters = (new VisualizationSettingsFactory()).GetVisualizationParameters();
        }

        public void SetParticleMovementSettings(InitializationParameters parameters)
        {
            parameters.TimeStep = 0.002f;
            parameters.TotalSteps = 400;
            parameters.Randi = new Random(0);
            parameters.ParticleGenerator = new CenterParticleGenerator(new Random(0));
            parameters.ParticleMoverFactory = new GravityPointMoverFactory(new Random(0), 0.2f, 0.5f);
        }

        public void SetCostStorageSettings(InitializationParameters parameters)
        {
            parameters.CostStorage = new AllCostReviewer(
                new MaxCostStorage(dic => dic.Keys.Contains(ParticleMovementCost.GetParticleMovementCostType()) ?
                    dic[ParticleMovementCost.GetParticleMovementCostType()] :
                    0d));
            parameters.VisualizationParameters.CostName = "MaxCost";
            parameters.ComCostFactory = new SimpleComCostFactory();
        }

        public void SetBasicSettings(InitializationParameters parameters)
        {
            SetSizeSettings(parameters);
            SetVisualizationSettings(parameters);
            SetParticleMovementSettings(parameters);
            SetCostStorageSettings(parameters);
        }
    }
}