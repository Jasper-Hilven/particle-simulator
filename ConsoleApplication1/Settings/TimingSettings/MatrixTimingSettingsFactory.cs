using ParticleSimulation.FieldCalculation.Cost;
using ParticleSimulation.Initialization;
using ParticleSimulation.Settings.HelperSettings;
using SharpDX;

namespace ParticleSimulation.Settings.TimingSettings
{
    public class MatrixTimingSettingsFactory
    {
        private readonly BasicSettingsFactory basicSettings;
        private readonly CostSettingsFactory costSettingsFactory;
        private readonly ParticlePositionFactory particlePositionFactory;
        private readonly SimulationTypeFactory simulationTypeFactory;

        public MatrixTimingSettingsFactory(BasicSettingsFactory basicSettings, CostSettingsFactory costSettingsFactory,
            SimulationTypeFactory simulationTypeFactory, ParticlePositionFactory particlePositionFactory)
        {
            this.basicSettings = basicSettings;
            this.costSettingsFactory = costSettingsFactory;
            this.simulationTypeFactory = simulationTypeFactory;
            this.particlePositionFactory = particlePositionFactory;
        }

        public InitializationParameters GetTimingInitializationParameters()
        {
            var retPar = basicSettings.InitializationParameters(1);
            simulationTypeFactory.SetMatrixSettings(retPar,2,6);
            costSettingsFactory.SetHelsimCombinedEstimation(retPar);
            const float ballCenter = 0.016675365f;
            const float ballRadius = 0.00193649f;
            particlePositionFactory.SetBallDistribution(retPar, new Vector3(ballCenter, ballCenter, ballCenter),
                ballRadius, 0.03f);
            retPar.SectionSize = new Vector3(0.03335073f, 0.03335073f, 0.03335073f);
            retPar.XCores = 2;
            retPar.XSections = 16;
            retPar.YCores = 2;
            retPar.YSections = 16;
            retPar.ZCores = 1;
            retPar.ZSections = 32;
            retPar.TotalSteps = 30;
            retPar.ParticlesPerCell = 216;
            retPar.ParticleReductionFactor = 1;
            retPar.CellsPerSectionSingleDim = 1;
            retPar.ComCostFactory = new CohesionComCalcFactory();
            retPar.VisualizationParameters.MainOutputPath = @"C:\Users\Jasper\Documents\School\Thesis\MATRIXTIMING" +
                                                            "\\";
            retPar.VisualizationParameters.MergedPictureAddPath = "MergedParth\\";
            retPar.VisualizationParameters.VideoAddPath = "Video\\";
            retPar.VisualizationParameters.VideoName = "Video";

            return retPar;
        }
    }
}