using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ParticleSimulation.Initialization;
using ParticleSimulation.Settings.HelperSettings;

namespace ParticleSimulation.Settings.PaperSettings
{
    class AllSettings
    {
        private Solution solution;
        private Validation validation;
        private string mainPath;

        public AllSettings(string mainPath)
        {
            this.mainPath = mainPath + @"ThesisFinalOut\";
        }

        public IEnumerable<InitializationParameters> GetPaperParameters()
        {
            solution = new Solution(new SimulationTypeFactory(), new BasicSettingsFactory(),
                new CostSettingsFactory(),mainPath);
            validation = new Validation(new BasicSettingsFactory(), mainPath);
            //return solution.GetSolutionParameters();
            return validation.GetParameters();
        }
    }
}
