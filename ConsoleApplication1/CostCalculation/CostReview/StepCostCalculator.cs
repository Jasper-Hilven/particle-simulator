using System.Collections.Generic;
using ParticleSimulation.Structuring;

namespace ParticleSimulation.CostCalculation.CostReview
{
   /* public class StepCostCalculator:ICostReviewer
    {
   /*     private readonly ICostReviewer decoCostReviewer;
        
        public StepCostCalculator(ICostReviewer decoCostReviewer)
        {
            this.decoCostReviewer = decoCostReviewer;
        }

        


        public Dictionary<ICore, Dictionary<ICostType, double>> GetCost(int step)
        {
            var costs = decoCostReviewer.GetCost(step);
            var sumCosts = new Dictionary<ICore, Dictionary<ICostType, double>>();
            foreach (var cost in costs)
            {
                sumCosts.Add(cost.Key,new Dictionary<ICostType, double>());
                foreach (var corecost in cost.Value)
                {
                    var type = corecost.Key;
                    var va = corecost.Value;
                    sumCosts[cost.Key].Add(type, va);
                    if (!(type is ISubSortCost)) continue;
                    double init; 
                    sumCosts[cost.Key].TryGetValue(type,out init);
                    sumCosts[cost.Key].Remove(type);
                    sumCosts[cost.Key].Add(type, va+init);
                }
            }

            return sumCosts;
        }
    }

    public interface ISubSortCost : ICostType
    {
    }

    public class SortCost : ICostType
    {
        protected bool Equals(SortCost other)
        {
            return (other != null);
        }

        public override int GetHashCode()
        {
           return 62237;
        }

        public string GetCostName()
        {
            return "totalSortingCost";
        }

        public override bool Equals(object obj)
        {
            return Equals(obj as SortCost);
        }
    }
*/
}