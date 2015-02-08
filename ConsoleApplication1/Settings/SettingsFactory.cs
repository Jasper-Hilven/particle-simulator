using System.Collections.Generic;
using System.Diagnostics;
using ParticleSimulation.Initialization;
using ParticleSimulation.Settings.ExperimentSettings;
using ParticleSimulation.Settings.HelperSettings;
using ParticleSimulation.Settings.PaperSettings;
using ParticleSimulation.Settings.Presentation;
using ParticleSimulation.Settings.TimingSettings;

namespace ParticleSimulation.Settings
{
    public class SettingsFactory
    {
        private readonly BasicSettingsFactory basicSettingsFactory = new BasicSettingsFactory();
        private readonly SimulationTypeFactory simulationTypeFactory = new SimulationTypeFactory();
        private readonly TimingSettingsFactory timingSettingsFactory =
            new TimingSettingsFactory(new BasicSettingsFactory(), new CostSettingsFactory(), new SimulationTypeFactory(),
                new ParticlePositionFactory());

        public IEnumerable<InitializationParameters> GetNextSettings()
        {

            return new AllSettings(@"C:\Users\Jasper\Documents\School\Thesis\OutputRuns\").GetPaperParameters();
            /*return new ArticleSettingsFactory(new SimulationTypeFactory(), new BasicSettingsFactory(),
                new CostSettingsFactory()).GetArticleParameters();
            /*yield break;
            return
                (new KMeansSettings2(new BasicSettingsFactory(), new CostSettingsFactory(),
                    new ParticlePositionFactory(), new SimulationTypeFactory())).GetCase5Settings();
        */
            /*for (int i = 0; i < 2; i++)
            {
                yield return
                    new TimingSettingsFactory(new BasicSettingsFactory(), new CostSettingsFactory(),
                        new SimulationTypeFactory(), new ParticlePositionFactory()).GetTimingInitializationParameters(2<<i);
                
            }*/
        }
    }
}