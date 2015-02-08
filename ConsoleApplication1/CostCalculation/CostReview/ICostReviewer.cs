using System.Collections.Generic;
using ParticleSimulation.Structuring;

namespace ParticleSimulation.CostCalculation.CostReview
{
    public interface ICostReviewer
    {
        Dictionary<ICore, Dictionary<ICostType, double>> GetCost(int step);
    }
}