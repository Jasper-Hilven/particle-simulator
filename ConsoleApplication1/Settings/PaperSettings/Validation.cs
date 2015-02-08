using System;
using System.Collections.Generic;
using System.Drawing;
using ParticleSimulation.CostCalculation;
using ParticleSimulation.CostCalculation.CostReview;
using ParticleSimulation.CostCalculation.CostReviewStorage;
using ParticleSimulation.CostCalculation.CostStorage;
using ParticleSimulation.FieldCalculation.Cost;
using ParticleSimulation.Initialization;
using ParticleSimulation.Initialization.ParticleGeneration;
using ParticleSimulation.Moving;
using ParticleSimulation.Settings.HelperSettings;
using ParticleSimulation.Structuring;
using SharpDX;

namespace ParticleSimulation.Settings.PaperSettings
{
    class Validation
    {
        private readonly BasicSettingsFactory basicSettingsFactory;
        //private readonly CostSettingsFactory costSettings;
        private readonly SimulationTypeFactory simulationTypeFactory;
        private string solutionPath;

        public Validation(BasicSettingsFactory basicSettingsFactory, string outputPath)
        {
            this.basicSettingsFactory = basicSettingsFactory;
            //  this.costSettings = costSettings;
            //  this.simulationTypeFactory = simulationTypeFactory;
            simulationTypeFactory = new SimulationTypeFactory();
            this.solutionPath = outputPath + @"validation\";

        }

        //Focus on scaling
        //Focus on specific properties
        //Focus
        //make finalCostStorageReviewer

        public IEnumerable<InitializationParameters> GetParameters()
        {

            foreach (var VARIABLE in setKmParameters())
            {
                yield return VARIABLE;
            }
            yield break;

            /* foreach (var initializationParameterse in SetNoCostSettings())
             {
                yield return initializationParameterse;
             }*/
            foreach (var initializationParameterse in setMatrixParameters())
            {
                yield return initializationParameterse;
            }


            for (int scale = 0; scale < 8 /*6*/; scale++)
            {
                var tScale = 2 << scale;
                for (int simulationType = 0; simulationType < 1/*6*/; simulationType++)
                {
                    /*for (int particleSituation = 2; particleSituation < 3; particleSituation++)
                    {
                        var particleSettings = basicSettingsFactory.InitializationParameters(tScale);
                        particleSettings.VisualizationParameters.MainOutputPath =solutionPath;
                        particleSettings.VisualizationParameters.MainOutputPath += @"Scale"+scale+@"\";
                        particleSettings.CellsPerSectionSingleDim = 4;
                        particleSettings.TimeStep = 0.02f;
                        particleSettings.XSections = 4;
                        particleSettings.YSections = 4;
                        particleSettings.ZSections = 4;
                        setSimulationSettings(simulationType, particleSettings);
                        setSituationSettings(particleSituation, particleSettings);
                        SetCostSettings(particleSettings);
                        yield return particleSettings;
                    }*/
                }
            }

        }

        private IEnumerable<InitializationParameters> SetFBSettings()
        {
            for (int scale = 0; scale < 3 /*6*/; scale++)
            {
                var timeStep = 0.01f;
                var tScale = 2 << scale;
                for (int simulationType = 1; simulationType < 2/*6*/; simulationType++)
                {
                    for (int particleSituation = 2; particleSituation < 3; particleSituation += 2)
                    {
                        var nodesSingleDim = 3;
                        var reduxFactor = 1;
                        var particleSettings = basicSettingsFactory.InitializationParameters(nodesSingleDim);
                        particleSettings.VisualizationParameters.MainOutputPath = solutionPath;
                        particleSettings.VisualizationParameters.MainOutputPath += @"FBSettings" + scale + @"\";
                        particleSettings.CellsPerSectionSingleDim = 8 / reduxFactor;
                        particleSettings.TimeStep = 0.02f;
                        particleSettings.XSections = 1;
                        particleSettings.YSections = 1;
                        particleSettings.ZSections = 1;
                        particleSettings.TotalSteps = 200 / reduxFactor * reduxFactor;
                        setSimulationSettings(simulationType, particleSettings);
                        var cellSize = 1 / ((float)particleSettings.XSections * particleSettings.CellsPerSectionSingleDim * nodesSingleDim);
                        setSituationSettings(particleSituation, particleSettings, ((float)cellSize) / timeStep);
                        SetCostSettings(particleSettings);
                        yield return particleSettings;
                    }
                }
            }
        }

        private IEnumerable<InitializationParameters> setMatrixParameters()
        {

            var particleSettings2 = InitializationParameterBigMatrix();
            // yield return particleSettings2;
            //yield break;




            for (int i = 0; i < 1; i += 2)
            {
                for (int j = 3; j >= 3; j--)
                {
                    var times = (2 << j) / 2;

                    Console.Out.WriteLine("i,j,times = " + i + ", " + j + ", " + times);

                    var timeStep = 0.01f;
                    var nodesSingleDim = 5;
                    var reduxFactor = 1;
                    var MyCellSize = 24;
                    var particleSettings = basicSettingsFactory.InitializationParameters(nodesSingleDim);
                    particleSettings.VisualizationParameters.MainOutputPath = solutionPath;
                    // particleSettings.VisualizationParameters.MainOutputPath += @"MatSettings" + j + @"\";
                    particleSettings.VisualizationParameters.MainOutputPath += @"MatSettingsDouble" + j + @"\";
                    particleSettings.CellsPerSectionSingleDim = 1;//8 / reduxFactor;
                    particleSettings.TimeStep = 0.02f;
                    particleSettings.XSections = 1;
                    particleSettings.YSections = 1;
                    particleSettings.ZSections = 1;
                    particleSettings.TotalSteps = 90 / reduxFactor * reduxFactor;
                    var cellSize = 1 /
                                   ((float)particleSettings.XSections * particleSettings.CellsPerSectionSingleDim *
                                    nodesSingleDim);
                    if (i == 0)
                    {
                        //Explosion
                        simulationTypeFactory.SetMatrixSettings(particleSettings, times, 1, MyCellSize);
                        setSituationSettings(i, particleSettings, ((float)cellSize) / timeStep / 10f);
                    }
                    else
                    {
                        //Smooth
                        simulationTypeFactory.SetMatrixSettings(particleSettings, times, 1, MyCellSize);
                        setSituationSettings(i, particleSettings, ((float)cellSize) / timeStep);
                    }
                    SetCostSettings(particleSettings);
                    yield return particleSettings;
                }
            }
        }

        private InitializationParameters InitializationParameterBigMatrix()
        {
            var times2 = (2 << 2) / 2;
            var timeStep2 = 0.01f;
            var nodesSingleDim2 = 4;
            var reduxFactor2 = 1;
            var cellnbP = 16;
            var particleSettings2 = basicSettingsFactory.InitializationParameters(nodesSingleDim2);
            particleSettings2.VisualizationParameters.MainOutputPath = solutionPath;
            particleSettings2.VisualizationParameters.SnapTime = 1;
            particleSettings2.VisualizationParameters.ParticleVisualizationSize = new Point(800, 800);
            particleSettings2.VisualizationParameters.MainOutputPath += @"MatSettingsLarge" + @"\";
            particleSettings2.CellsPerSectionSingleDim = 1; //8 / reduxFactor;
            particleSettings2.TimeStep = 0.02f;
            particleSettings2.XSections = 1;
            particleSettings2.YSections = 1;
            particleSettings2.ZSections = 1;
            particleSettings2.TotalSteps = 200 / reduxFactor2 * reduxFactor2;
            var cellSize2 = 1 /
                            ((float)particleSettings2.XSections * particleSettings2.CellsPerSectionSingleDim *
                             nodesSingleDim2);
            //Explosion
            simulationTypeFactory.SetMatrixSettings(particleSettings2, times2, 1, cellnbP);
            setSituationSettings(0, particleSettings2, ((float)cellSize2) / timeStep2 / 10f);
            SetCostSettings(particleSettings2);
            return particleSettings2;
        }

        private IEnumerable<InitializationParameters> setKmParameters()
        {
            for (int i = 0; i < 3; i += 4)
            {
                for (int j = 5; j < 15; j+=5)
                {



                    var timeStep = 0.01f;
                    var nodesSingleDim = 15;
                    var reduxFactor = 1;
                    var particleSettings = basicSettingsFactory.InitializationParameters(nodesSingleDim);
                    particleSettings.VisualizationParameters.MainOutputPath = solutionPath;
                    particleSettings.VisualizationParameters.MainOutputPath += @"KMSettings" + "MD"+j +@"\";
                    particleSettings.CellsPerSectionSingleDim = 8 / reduxFactor;
                    particleSettings.TimeStep = 0.02f;
                    particleSettings.XSections = 1;
                    particleSettings.YSections = 1;
                    particleSettings.ZSections = 1;
                    particleSettings.TotalSteps = 1000 / reduxFactor * reduxFactor;
                    simulationTypeFactory.SetKMeansSettings(particleSettings);
                    var cellSize = 1 /
                                   ((float)particleSettings.XSections * particleSettings.CellsPerSectionSingleDim *
                                    nodesSingleDim);
                    if (i == 0)
                        setSituationSettings(i, particleSettings, ((float)cellSize) / timeStep / 10);
                    else
                        setSituationSettings(i, particleSettings, ((float)cellSize) / timeStep);
                    SetCostSettings(particleSettings);
                    yield return particleSettings;
                }
            }
        }


        private void SetCostSettings(InitializationParameters particleSettings)
        {
            particleSettings.CostStorage = new FinalReviewCostStorage();
            particleSettings.ComCostFactory = new CohesionComCalcFactory();
            particleSettings.VisualizationParameters.CostName = "Costs";
        }

        private void setSimulationSettings(int simulationType, InitializationParameters particleSettings)
        {
            //NoSorting
            if (simulationType == 0)
            {
                simulationTypeFactory.SetHelsimSettings(particleSettings);
                particleSettings.VisualizationParameters.MainOutputPath += @"NoSorting\";
            }
            //StrictBoundary
            if (simulationType == 1)
            {
                simulationTypeFactory.SetStrictBoundarySettings(particleSettings);
                particleSettings.VisualizationParameters.MainOutputPath += @"StrictBoundary\";
            }
            //Matrix 1,3,6
            if (simulationType >= 2 && simulationType <= 4)
            {
                int timesSorting = new List<int> { 1, 3, 6 }[simulationType - 2];
                simulationTypeFactory.SetMatrixSettings(particleSettings, timesSorting, 6);
                particleSettings.VisualizationParameters.MainOutputPath += @"MatrixSort" + timesSorting + @"\";
            }
            if (simulationType >= 5)
            {
                throw new NotImplementedException();
            }

        }

        private void setSituationSettings(int particleSituation, InitializationParameters particleSettings, float maxSpeed)
        {
            //Explosion
            if (particleSituation == 0)
            {
                particleSettings.ParticleGenerator = new BallParticleGenerator(new Vector3(0.5f), 0.01f, new Random(0), maxSpeed);
                particleSettings.ParticleMoverFactory = new BiasedParticleMoverFactory(new Random(0), maxSpeed, new Vector3(), 0f);
                particleSettings.VisualizationParameters.MainOutputPath += @"Explosion" + @"\";
            }

            //Sinusoidal movement
            if (particleSituation == 1)
            {
                particleSettings.ParticleGenerator = new LocalParticleGenerator(new Random(0));
                particleSettings.ParticleMoverFactory = new WaveParticleMoverFactory(new Random(0), 5f);
                particleSettings.VisualizationParameters.MainOutputPath += @"Sinusoidal" + @"\";
            }
            //Smooth distribution
            if (particleSituation == 2)
            {
                particleSettings.ParticleGenerator = new LocalParticleGenerator(new Random(0));
                particleSettings.ParticleMoverFactory = new BiasedParticleMoverFactory(new Random(0), maxSpeed, new Vector3(), 1f);
                particleSettings.VisualizationParameters.MainOutputPath += @"SmoothDistributed" + @"\";
            }
            if (particleSituation > 2)
                throw new Exception();

        }

        private IEnumerable<InitializationParameters> SetNoCostSettings()
        {
            for (int scale = 0; scale < 3 /*6*/; scale++)
            {
                var timeStep = 0.01f;
                var tScale = 2 << scale;
                for (int simulationType = 0; simulationType < 1/*6*/; simulationType++)
                {
                    for (int particleSituation = 2; particleSituation < 3; particleSituation++)
                    {
                        var nodesSingleDim = 100;
                        var particleSettings = basicSettingsFactory.InitializationParameters(nodesSingleDim);
                        particleSettings.VisualizationParameters.MainOutputPath = solutionPath;
                        particleSettings.VisualizationParameters.MainOutputPath += @"NoCostSettings" + scale + @"\";
                        particleSettings.CellsPerSectionSingleDim = 8;
                        particleSettings.TimeStep = 0.02f;
                        particleSettings.XSections = 1;
                        particleSettings.YSections = 1;
                        particleSettings.ZSections = 1;
                        particleSettings.TotalSteps = 4000;
                        setSimulationSettings(simulationType, particleSettings);
                        var cellSize = 1 / ((float)particleSettings.XSections * particleSettings.CellsPerSectionSingleDim * 100);
                        setSituationSettings(particleSituation, particleSettings, ((float)cellSize) / timeStep);
                        particleSettings.ParticleGenerator = new SingleNodeLocalParticleGenerator(new Random(0));
                        SetCostSettings(particleSettings);
                        yield return particleSettings;
                    }
                }
            }
        }

        public class FinalReviewCostStorage : IReviewCostStorage
        {
            private SmallCostStorage smStorage;
            private ICostReviewer maxStepCostCalc;

            public FinalReviewCostStorage()
            {
                smStorage = new SmallCostStorage();
                maxStepCostCalc = new MaxStepCostCalculator(smStorage);
            }

            public void AddCost(ICostType cType, ICore core, int step, double cost)
            {
                smStorage.AddCost(cType, core, step, cost);
            }


            //Op 1 core wil ik maximum iedere cost van iedere step.
            public Dictionary<ICore, Dictionary<ICostType, double>> GetCost(int step)
            {
                return maxStepCostCalc.GetCost(step);
            }
        }
    }
}
