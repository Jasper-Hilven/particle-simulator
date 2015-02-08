using ParticleSimulation.Structuring;

namespace ParticleSimulation.CostCalculation.CostStorage
{
    public interface ICostStorage
    {
        //Dictionary<int, Dictionary<ICore, Dictionary<ICostType, double>>> Costs { get; }
        void AddCost(ICostType cType, ICore core, int step, double cost);
    }

    
}