using ParticleSimulation.Initialization;
using ParticleSimulation.Settings.HelperSettings;

namespace ParticleSimulation.Settings.TimingSettings
{
    internal class KMeansTimingSettingsFactory
    {
        private readonly MatrixTimingSettingsFactory matrixTimingSettingsFactory;
        private readonly SimulationTypeFactory simFactory = new SimulationTypeFactory();

        public KMeansTimingSettingsFactory()
        {
            matrixTimingSettingsFactory = new MatrixTimingSettingsFactory(new BasicSettingsFactory(),
                new CostSettingsFactory(), new SimulationTypeFactory(), new ParticlePositionFactory());
        }

        public InitializationParameters GetTimingInitializationParameters()
        {
            var timingInitializationParameters =
                matrixTimingSettingsFactory.GetTimingInitializationParameters();
            simFactory.SetKMeansSettings(timingInitializationParameters);
            timingInitializationParameters.VisualizationParameters.MainOutputPath =
                @"C:\Users\Jasper\Documents\School\Thesis\KMeansTiming";
            return timingInitializationParameters;
        }
    }
}