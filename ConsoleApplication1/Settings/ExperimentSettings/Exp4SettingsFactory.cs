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
    public class Exp4SettingsFactory
    {
        private readonly BasicSettingsFactory basicSettings;
        private readonly CostSettingsFactory costSettingsFactory;
        private readonly ParticlePositionFactory particlePositionFactory;
        private readonly SimulationTypeFactory simulationTypeFactory;

        public Exp4SettingsFactory(BasicSettingsFactory basicSettings, CostSettingsFactory costSettingsFactory,
            ParticlePositionFactory particlePositionFactory, SimulationTypeFactory simulationTypeFactory)
        {
            this.basicSettings = basicSettings;
            this.costSettingsFactory = costSettingsFactory;
            this.particlePositionFactory = particlePositionFactory;
            this.simulationTypeFactory = simulationTypeFactory;
        }

        public IEnumerable<InitializationParameters> GetCase1Settings()
        {
            
                for (var i = 0; i < 3; i++)
                {
                    var retPar = basicSettings.InitializationParameters(1);
                    costSettingsFactory.SetHelsimCombinedEstimation(retPar);
                    var pm = new BiasedParticleMoverFactory(new Random(0), 0.1f, new Vector3(), 0.0f);
                    retPar.ParticleMoverFactory = pm;
                    particlePositionFactory.SetDistributedPositions(parameters: retPar);
                    retPar.SectionSize = new Vector3(1f, 1f, 1f);
                    SetSizeSettings(retPar);
                    retPar.ParticleReductionFactor = 1;
                    retPar.CellsPerSectionSingleDim = 1;
                    retPar.ComCostFactory = new CohesionComCalcFactory();
                    retPar.VisualizationParameters.SnapTime = 1;
                    retPar.VisualizationParameters.ParticleVisualizationSize = new Point(1000,1000);
                    retPar.VisualizationParameters.MainOutputPath =
                        @"C:\Users\Jasper\Documents\School\Thesis\CASE4SETTINGS" + i +"_"+ "\\";
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

        private static void SetSizeSettings(InitializationParameters retPar)
        {
            retPar.XCores = 4;
            retPar.XSections = 4;
            retPar.YCores = 1;
            retPar.YSections = 1;
            retPar.ZCores = 4;
            retPar.ZSections = 4;
            retPar.TotalSteps = 200;
            retPar.ParticlesPerCell = 216;
        } 
    }
}