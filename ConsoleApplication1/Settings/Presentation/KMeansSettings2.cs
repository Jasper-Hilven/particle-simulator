using System;
using System.Collections.Generic;
using System.Drawing;
using ParticleSimulation.FieldCalculation.Cost;
using ParticleSimulation.Initialization;
using ParticleSimulation.Moving;
using ParticleSimulation.Settings.HelperSettings;
using SharpDX;

namespace ParticleSimulation.Settings.Presentation
{
    public class KMeansSettings2
    {
        private readonly BasicSettingsFactory basicSettings;
        private readonly CostSettingsFactory costSettingsFactory;
        private readonly ParticlePositionFactory particlePositionFactory;
        private readonly SimulationTypeFactory simulationTypeFactory;

        public KMeansSettings2(BasicSettingsFactory basicSettings, CostSettingsFactory costSettingsFactory, ParticlePositionFactory particlePositionFactory, SimulationTypeFactory simulationTypeFactory)
        {
            this.basicSettings = basicSettings;
            this.costSettingsFactory = costSettingsFactory;
            this.particlePositionFactory = particlePositionFactory;
            this.simulationTypeFactory = simulationTypeFactory;
        }

        public IEnumerable<InitializationParameters> GetCase5Settings()
        {
            for (int i = 0; i < 2; i++)
            {
                var retPar = basicSettings.InitializationParameters(1);
                costSettingsFactory.SetHelsimCombinedEstimation(retPar);
                retPar.ParticleMoverFactory = new BiasedParticleMoverFactory(new Random(0), 1000f, new Vector3(), 500f);
                particlePositionFactory.SetBallDistribution(retPar, new Vector3(0.5f, 0.5f, 0.5f), 0.5f,0.03f);
                SetSizeSettings(retPar);
                retPar.ParticleReductionFactor = 1;
                retPar.CellsPerSectionSingleDim = 1;
                retPar.ComCostFactory = new CohesionComCalcFactory();
                retPar.VisualizationParameters.SnapTime = 1;
                retPar.VisualizationParameters.ParticleVisualizationSize = new Point(300, 300);
                retPar.VisualizationParameters.MainOutputPath =
                    @"C:\Users\Jasper\Documents\School\Thesis\ArticleMarchKMeansDemo2_" + i + "\\";
                retPar.VisualizationParameters.MergedPictureAddPath = "MergedParth\\";
                retPar.VisualizationParameters.VideoAddPath = "Video\\";
                retPar.VisualizationParameters.VideoName = "Video";
                simulationTypeFactory.SetKMeansSettings(retPar);
                if (i == 1)
                    simulationTypeFactory.SetMatrixSettings(retPar,1,6); //ONLY ONCE SO NB of comparisons is about equal
                retPar.SectionSize = new Vector3(1f, 1f, 1f);
                yield return retPar;
            }


        }
        private static void SetSizeSettings(InitializationParameters retPar)
        {
            retPar.XCores = 4;
            retPar.XSections = 1;
            retPar.YCores = 1;
            retPar.YSections = 1;
            retPar.ZCores = 4;
            retPar.ZSections = 1;
            retPar.TotalSteps = 300;
            retPar.ParticlesPerCell = 216;
        }

    }
}