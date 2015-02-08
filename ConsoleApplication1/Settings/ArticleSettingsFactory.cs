using System;
using System.Collections.Generic;
using System.Net.Configuration;
using ParticleSimulation.CostCalculation.CostStorage;
using ParticleSimulation.FieldCalculation.Cost.Types;
using ParticleSimulation.Initialization;
using ParticleSimulation.Initialization.ParticleGeneration;
using ParticleSimulation.Moving;
using ParticleSimulation.Settings.HelperSettings;
using ParticleSimulation.Tactics.KMeans.KMeansSectioning;
using ParticleSimulation.Tactics.KMeans.KMeansSorting;
using SharpDX;

namespace ParticleSimulation.Settings
{
    /// <summary>
    ///     The settings for the article.
    /// </summary>
    public class ArticleSettingsFactory
    {
        private readonly BasicSettingsFactory basicSettingsFactory;
        private readonly CostSettingsFactory costSettings;
        private readonly SimulationTypeFactory simulationTypeFactory;

        public ArticleSettingsFactory(SimulationTypeFactory settingsFactory, BasicSettingsFactory basicSettingsFactory,
            CostSettingsFactory costSettings)
        {
            simulationTypeFactory = settingsFactory;
            this.basicSettingsFactory = basicSettingsFactory;
            this.costSettings = costSettings;
        }

        public IEnumerable<InitializationParameters> GetArticleParameters()
        {
            //yield return GetSingleInitializationParameter(0, 0);
            for (var i = 0; i < 1; i++)
            {
                for (var j = 4; j < 7; j++)
                {
                    yield return GetSingleInitializationParameter(i, j);
                }
            }
            /**/
        }

        private InitializationParameters GetSingleInitializationParameter(int i, int j)
        {
            var settings = basicSettingsFactory.InitializationParameters(10);
            settings = costSettings.SetHelsimCombinedEstimation(settings);
            if (i == 0)
            {
                settings.ParticleGenerator = new LocalParticleGenerator(new Random(0));
                //first we put them all distributed
                settings.ParticleMoverFactory = new WaveParticleMoverFactory(new Random(0), 1f);
            }
            if (i == 1)
            {
                settings.ParticleGenerator = new LocalParticleGenerator(new Random(0));
                settings.ParticleMoverFactory = new BiasedParticleMoverFactory(new Random(0), 1f, new Vector3(0.3f), 3f);
            }

            throw new Exception("Look to addHelsimCostStorage to avoid duplication");
            settings.CostStorage =
                new MaxCostStorage(null/*(o =>
                    o[ParticleMovementCost.GetParticleMovementCostType()] +
                    (o.ContainsKey(ConstantSolvingCost.GetConstantSolvingCost())
                        ? o[ConstantSolvingCost.GetConstantSolvingCost()]
                        : 0) +
                    (o.ContainsKey(ParticleWrongPlaceCommunication.GetParticleWrongPlaceCommunicationCost())
                        ? o[ParticleWrongPlaceCommunication.GetParticleWrongPlaceCommunicationCost()]
                        : 0))*/);

            /*if (j == 0)
                simulationTypeFactory.SetMatrixSettings(settings,1);
            if (j == 1)
                simulationTypeFactory.SetHelsimSettings(settings);
            if (j == 2)
                simulationTypeFactory.SetGNeighbourSettings(settings);
            if (j == 3)
            */
            simulationTypeFactory.SetStrictBoundarySettings(settings);
            if (j == 4)
            {
                simulationTypeFactory.SetKMeansSettings(settings);
                settings.SorterFactory = new SingleDimKMeansSorterFactory(0);
                settings.SectionFactory = new KMeansSectionFactory(0, 0.1f, 100f);
            }
            if (j == 5)
            {
                simulationTypeFactory.SetKMeansSettings(settings);
                settings.SorterFactory = new KMeansSorterFactory(0);
                settings.SectionFactory = new KMeansSectionFactory(0, 0.1f, 100f);
            }
            if (j == 6)
            {
                simulationTypeFactory.SetKMeansSettings(settings);
                settings.SorterFactory = new SingleDimKMeansSorterFactory(0);
                settings.SectionFactory = new KMeansSectionFactory(0, 0.1f, 0.5f);
            }
            settings.TotalSteps = 2000;
            settings.VisualizationParameters.SnapTime = 1;
            settings.VisualizationParameters.MainOutputPath =
                @"C:\Users\Jasper\Documents\School\Thesis\OutputRuns\article\situation" + i + "algorithm" + j;
            return settings;
        }
    }
}