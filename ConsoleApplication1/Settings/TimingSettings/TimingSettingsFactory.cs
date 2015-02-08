using ParticleSimulation.FieldCalculation.Cost;
using ParticleSimulation.Initialization;
using ParticleSimulation.Settings.HelperSettings;
using SharpDX;

namespace ParticleSimulation.Settings.TimingSettings
{
    /// <summary>
    ///     Simulation to check whether the timing from the excel "ResultsTomSparseArray.ods" matches the simulation.
    /// </summary>
    public class TimingSettingsFactory
    {
        private readonly BasicSettingsFactory basicSettings;
        private readonly CostSettingsFactory costSettingsFactory;
        private readonly ParticlePositionFactory particlePositionFactory;
        private readonly SimulationTypeFactory simulationTypeFactory;

        public TimingSettingsFactory(BasicSettingsFactory basicSettings, CostSettingsFactory costSettingsFactory,
            SimulationTypeFactory simulationTypeFactory, ParticlePositionFactory particlePositionFactory)
        {
            this.basicSettings = basicSettings;
            this.costSettingsFactory = costSettingsFactory;
            this.simulationTypeFactory = simulationTypeFactory;
            this.particlePositionFactory = particlePositionFactory;
        }

        public InitializationParameters GetTimingInitializationParameters(int nbProcessesSingleDim)
        {
            var retPar = basicSettings.InitializationParameters(1);
            simulationTypeFactory.SetHelsimSettings(retPar);
            costSettingsFactory.SetHelsimCombinedEstimation(retPar);
            const float ballCenter = 0.016675365f;
            const float ballRadius = 0.000897554f;
            particlePositionFactory.SetBallDistribution(retPar, new Vector3(ballCenter, ballCenter, ballCenter),
                ballRadius, 0.03f);
            //TODO simulatieruimte is maar deel van de ruimte
            retPar.SectionSize = new Vector3(0.03335073f,0.03335073f,0.03335073f);
            retPar.XCores = nbProcessesSingleDim;
            retPar.XSections = 2;
            retPar.YCores = nbProcessesSingleDim;
            retPar.YSections = 2;
            retPar.ZCores = nbProcessesSingleDim;
            retPar.ZSections = 2;
            retPar.TotalSteps = 30; 
            retPar.ParticlesPerCell = 432*4*4*4;
            retPar.ParticleReductionFactor = 64;
            retPar.CellsPerSectionSingleDim = 16;
            retPar.ComCostFactory = new CohesionComCalcFactory();
            retPar.VisualizationParameters.MainOutputPath = @"C:\Users\Jasper\Documents\School\Thesis\TIMINGRepairedEqualBug" +
                                                            nbProcessesSingleDim + "\\";
            retPar.VisualizationParameters.MergedPictureAddPath = "MergedParth\\";
            retPar.VisualizationParameters.VideoAddPath = "Video\\";
            retPar.VisualizationParameters.VideoName = "Video";
  
            return retPar;
        }
    }
}