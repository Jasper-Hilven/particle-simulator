using ParticleSimulation.CostCalculation;

namespace ParticleSimulation.FieldCalculation.Cost.Types
{
    //Fact that a message needs to be send from A

    public class ParticleWPCCoresHomeCoreRole : ICostType
    {
        private static ParticleWPCCoresHomeCoreRole pwpc = new ParticleWPCCoresHomeCoreRole();

        public static ParticleWPCCoresHomeCoreRole GetParticleWpcCoresSend()
        {
            return pwpc;
        }

        private ParticleWPCCoresHomeCoreRole()
        {
        }

        public string GetCostName()
        {
            return "ICComSend";
        }
    }
}