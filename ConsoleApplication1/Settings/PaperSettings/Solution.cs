using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ParticleSimulation.Initialization;
using ParticleSimulation.Initialization.ParticleGeneration;
using ParticleSimulation.Moving;
using ParticleSimulation.Settings.HelperSettings;
using ParticleSimulation.Structuring.Sectioning;
using ParticleSimulation.Tactics.KMeans;
using ParticleSimulation.Tactics.KMeans.KMeansSectioning;
using ParticleSimulation.Tactics.KMeans.KMeansSorting;
using SharpDX;

namespace ParticleSimulation.Settings.PaperSettings
{
    class Solution
    {
        private readonly BasicSettingsFactory basicSettingsFactory;
        private readonly CostSettingsFactory costSettings;
        private readonly SimulationTypeFactory simulationTypeFactory;
        private string solutionPath;

        public Solution(SimulationTypeFactory settingsFactory, BasicSettingsFactory basicSettingsFactory,
            CostSettingsFactory costSettings, string outputPath)
        {
            simulationTypeFactory = settingsFactory;
            this.basicSettingsFactory = basicSettingsFactory;
            this.costSettings = costSettings;
            this.solutionPath = outputPath + @"solution\";
        }

        public IEnumerable<InitializationParameters> GetSolutionParameters()
        {
            return GetKMeansParameters();
        }

        public IEnumerable<InitializationParameters> GetKMeansParameters()
        {
            for (int i = 0; i < 6; i++)
            {
                var settings = basicSettingsFactory.InitializationParameters(10);
                settings = costSettings.SetHelsimCombinedEstimation(settings);
                settings.ParticleGenerator = new LocalParticleGenerator(new Random(0));
                settings.ParticleMoverFactory = new BiasedParticleMoverFactory(new Random(0), 1f, new Vector3(0.3f), 3f);
                settings.VisualizationParameters.SnapTime = 1;
                simulationTypeFactory.SetKMeansSettings(settings);
                settings.SectionFactory = new KMeansSectionFactory(0, 0.2f, 3);
                settings.VisualizationParameters.MainOutputPath = solutionPath;
                if (i == 0)
                { settings.SorterFactory = new KMeansSorterFactory(0f);
                    settings.VisualizationParameters.MainOutputPath += @"problem1PlaceChanging\";
                }
                if (i == 1)
                { settings.SorterFactory = new SingleDimKMeansSorterFactory(0f); 
                    settings.VisualizationParameters.MainOutputPath += @"Fixed2Problems\"; }
                if (i >= 2)
                {
                    settings.SorterFactory = new SingleDimKMeansSorterFactory(0f);
                    settings.SectionFactory = new KMeansSectionFactory(0, 0.2f, 0.2f);
                    settings.VisualizationParameters.MainOutputPath += @"LimitedDistance"+ i + @"\";
                }
                yield return settings;
            }
        }
    }
}
