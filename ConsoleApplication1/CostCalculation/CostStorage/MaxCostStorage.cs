using System;
using System.Collections.Generic;
using ParticleSimulation.CostCalculation.CostReviewStorage;
using ParticleSimulation.Structuring;
using ParticleSimulation.Visualization.CostVisualization;

namespace ParticleSimulation.CostCalculation.CostStorage
{
    /*Stores the cores with the greatest cost. A cost formula must be given*/
    public class MaxCostStorage:IReviewCostStorage
    {
        private Func<Dictionary<ICostType, double>,double> formula;
        private readonly SmallCostStorage smallCostStorage = new SmallCostStorage();
        private readonly CostReviewStorage.CostStorage maxStorage = new CostReviewStorage.CostStorage();
        private int currentStep = 0;
        
        public MaxCostStorage(Func<Dictionary<ICostType, double>, double> formula)
        {
            this.formula = formula;
        }

        public void AddCost(ICostType cType, ICore core, int step, double cost)
        {
             if (step != currentStep)
                 StoreHighest(step);
            //NOW LETS RESET THE SMALL COST BY ADDING A COST OF A HIGHER STEP.
            smallCostStorage.AddCost(cType,core,step,cost);
        }

        private void StoreHighest(int step)
        {
            currentStep = step;
            double maxCost = -1;
            ICore maxCore = null;
            var goodStep = -1;
           
                foreach (var cStep in smallCostStorage.Costs.Keys)
                {
                    goodStep = cStep;
                    foreach (var pCore in smallCostStorage.Costs[cStep].Keys)
                    {
                        var potMax = formula(smallCostStorage.Costs[cStep][pCore]);
                        if (potMax > maxCost)
                        {
                            maxCost = potMax;
                            maxCore = pCore;
                        }
                    }
                }
                foreach (var copyCost in smallCostStorage.Costs[goodStep][maxCore])
                {
                    
                    maxStorage.AddCost(copyCost.Key, maxCore, currentStep, copyCost.Value);
                }
            
        }
        public void FinishStorage()
        {
            StoreHighest(currentStep);
        }

        public Dictionary<ICore, Dictionary<ICostType, double>> GetCost(int step)
        {
            if(step > currentStep)
                throw new Exception("NO COST AVAILABLE");
            if (step == currentStep)
            {
                StoreHighest(currentStep);
                currentStep ++;
            }
            return maxStorage.GetCost(step);
        }
    }
}