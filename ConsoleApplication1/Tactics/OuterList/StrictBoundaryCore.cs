using System.Collections.Generic;
using ParticleSimulation.Structuring;
using ParticleSimulation.Structuring.Sectioning;

namespace ParticleSimulation.Tactics.OuterList
{
    
    public class StrictBoundaryCore : ICore
    {
        public IEnumerable<IParticleSection> Sections
        {
            get { return sections; }
        }
        private List<IParticleSection> sections = new List<IParticleSection>(); 
        public void AddSection(IParticleSection particleSection)
        {
            sections.Add(particleSection);
        }

    }
}