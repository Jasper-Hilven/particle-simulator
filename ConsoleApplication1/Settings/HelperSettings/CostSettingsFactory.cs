using Emgu.CV;
using ParticleSimulation.CostCalculation.CostReviewStorage;
using ParticleSimulation.CostCalculation.CostStorage;
using ParticleSimulation.FieldCalculation.Cost;
using ParticleSimulation.FieldCalculation.Cost.Types;
using ParticleSimulation.Initialization;
using ParticleSimulation.Moving;
using ParticleSimulation.Sorting.Cost;

namespace ParticleSimulation.Settings.HelperSettings
{
    public class CostSettingsFactory
    {
        public InitializationParameters SetHelsimCombinedEstimation(InitializationParameters retParameters)
        {
            retParameters.CostStorage =
                new MaxCostStorage((o =>
                    o[ParticleMovementCost.GetParticleMovementCostType()] +
                    (o.ContainsKey(ConstantSolvingCost.GetConstantSolvingCost())
                        ? o[ConstantSolvingCost.GetConstantSolvingCost()]
                        : 0) +
                    (o.ContainsKey(new SortingCostParticleSwap())
                        ? o[new SortingCostParticleSwap()]
                        : 0) +
                    (o.ContainsKey(new SortingCostParticleComparison())
                        ? o[new SortingCostParticleComparison()]
                        : 0) +
                    (o.ContainsKey(ParticleWpcCoresSectionCoreRole.GetParticleWpcCoresReceiveCost())
                        ? o[ParticleWpcCoresSectionCoreRole.GetParticleWpcCoresReceiveCost()]
                        : 0) +
                    (o.ContainsKey(ParticleWPCCoresHomeCoreRole.GetParticleWpcCoresSend())
                        ? o[ParticleWPCCoresHomeCoreRole.GetParticleWpcCoresSend()]
                        : 0)));
            retParameters.VisualizationParameters.CostName = "SetHelsimCombinedEstimation";
            retParameters.ComCostFactory = new SimpleComCostFactory();
            return retParameters;
        }

        public InitializationParameters SetFinalCombinedEstimation(InitializationParameters parameters)
        {
            parameters.CostStorage = new CostStorage(); 
            parameters.ComCostFactory = new CohesionComCalcFactory();
            return parameters;
        }

    }
}