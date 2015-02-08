using System.Collections.Generic;
using ParticleSimulation.Structuring;
using ParticleSimulation.Structuring.Sectioning;

namespace ParticleSimulation.Tactics.SideSort
{
    public class SideSortCore:ICore
    {
        public IEnumerable<IParticleSection> Sections { get { return sections; } }
        private readonly List<IParticleSection> sections; 
        public SideSortCore()
        {
            sections = new List<IParticleSection>();
        }

        public void AddSection(IParticleSection particleSection)
        {
            sections.Add(particleSection);
        }
    }
}