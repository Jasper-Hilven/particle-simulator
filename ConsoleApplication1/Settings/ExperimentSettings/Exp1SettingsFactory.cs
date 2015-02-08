using System;
using System.Collections.Generic;
using System.Drawing;
using ParticleSimulation.FieldCalculation.Cost;
using ParticleSimulation.Initialization;
using ParticleSimulation.Moving;
using ParticleSimulation.Settings.HelperSettings;
using SharpDX;

namespace ParticleSimulation.Settings.ExperimentSettings
{
    /// <summary>
    ///     All particles will be put on the right down corner.
    /// </summary>
    public class Exp1SettingsFactory
    {
        private readonly BasicSettingsFactory basicSettings;
        private readonly CostSettingsFactory costSettingsFactory;
        private readonly ParticlePositionFactory particlePositionFactory;
        private readonly SimulationTypeFactory simulationTypeFactory;

        public Exp1SettingsFactory(BasicSettingsFactory basicSettings, CostSettingsFactory costSettingsFactory,
            ParticlePositionFactory particlePositionFactory, SimulationTypeFactory simulationTypeFactory)
        {
            this.basicSettings = basicSettings;
            this.costSettingsFactory = costSettingsFactory;
            this.particlePositionFactory = particlePositionFactory;
            this.simulationTypeFactory = simulationTypeFactory;
        }

        public IEnumerable<InitializationParameters> GetCase1Settings()
        {
            for (var j = 1; j < 2; j++)
            {

                for (var i = 2; i < 3; i++)
                {
                    var retPar = basicSettings.InitializationParameters(1);
                    costSettingsFactory.SetHelsimCombinedEstimation(retPar);
                    var pm = new BiasedParticleMoverFactory(new Random(0), 0.1f, new Vector3(), 0.0f);
                    retPar.ParticleMoverFactory = pm;
                    particlePositionFactory.SetBallDistribution(retPar, new Vector3(0.8f, 0.8f, 0.8f), 0.15f, 0.03f);
                    retPar.SectionSize = new Vector3(1f, 1f, 1f);
                    SetSizeSettings(retPar);
                    retPar.ParticleReductionFactor = 1;
                    retPar.CellsPerSectionSingleDim = 1;
                    retPar.ComCostFactory = new CohesionComCalcFactory();
                    if(j == 1)
                        retPar.ComCostFactory = new SimpleComCostFactory();
                    retPar.VisualizationParameters.SnapTime = 1;
                    retPar.VisualizationParameters.ParticleVisualizationSize = new Point(9200,5000);
                    retPar.VisualizationParameters.MainOutputPath =
                        @"C:\Users\Jasper\Documents\School\Thesis\CASE1SETTINGS" + i +"_"+ j  + "\\";
                    retPar.VisualizationParameters.MergedPictureAddPath = "MergedParth\\";
                    retPar.VisualizationParameters.VideoAddPath = "Video\\";
                    retPar.VisualizationParameters.VideoName = "Video";
                    if (i%3 == 0)
                        simulationTypeFactory.SetMatrixSettings(retPar,1,6);
                    if (i%3 == 1)
                        simulationTypeFactory.SetHelsimSettings(retPar);
                    if (i%3 == 2)
                        simulationTypeFactory.SetKMeansSettings(retPar);
                    yield return retPar;
                }
            }
        }

        private static void SetSizeSettings(InitializationParameters retPar)
        {
            retPar.XCores = 16;
            retPar.XSections = 1;
            retPar.YCores = 16;
            retPar.YSections = 1;
            retPar.ZCores = 1;
            retPar.ZSections = 1;
            retPar.TotalSteps = 30;
            retPar.ParticlesPerCell = 216;
        }
    }
}