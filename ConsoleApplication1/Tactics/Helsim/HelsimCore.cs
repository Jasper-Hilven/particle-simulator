using System.Collections.Generic;
using ParticleSimulation.Structuring;
using ParticleSimulation.Structuring.Sectioning;

namespace ParticleSimulation.Tactics.Helsim
{
    public class HelsimCore:ICore
    {
        List<IParticleSection> sections = new List<IParticleSection>();
        public IEnumerable<IParticleSection> Sections
        {
            get { return sections; }
        }
        public void AddSection(IParticleSection particleSection)
        {
            sections.Add(particleSection);
        }
    }
}