using System.Collections.Generic;
using System.Linq;
using ParticleSimulation.Structuring.Sectioning;

namespace ParticleSimulation.Structuring
{
    public class SimulationStructure
    {
        public CoreGrid CoreGrid { get; set; }
        public IEnumerable<Particle> GetParticles()
        {
            return CoreGrid.GetCores().SelectMany(c => c.Sections.SelectMany(s => s.Particles));
        }

        public IEnumerable<IParticleSection> GetAllSections()
        {
            return CoreGrid.GetCores().SelectMany(o => o.Sections);
        }
    }
}