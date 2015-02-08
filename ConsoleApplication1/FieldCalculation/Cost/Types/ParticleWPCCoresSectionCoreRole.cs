using ParticleSimulation.CostCalculation;

namespace ParticleSimulation.FieldCalculation.Cost.Types
{
    //Fact that a message needs to be send to A

    public class ParticleWpcCoresSectionCoreRole : ICostType
    {
        private static ParticleWpcCoresSectionCoreRole pwpc = new ParticleWpcCoresSectionCoreRole();
        public static ParticleWpcCoresSectionCoreRole GetParticleWpcCoresReceiveCost()
        {
            return pwpc;
        }
        
        private ParticleWpcCoresSectionCoreRole()
        {

        }

        public string GetCostName()
        {
            return "ICComReceive";
        }
    }
}