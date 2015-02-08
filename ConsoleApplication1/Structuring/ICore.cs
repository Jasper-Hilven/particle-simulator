using System.Collections.Generic;
using ParticleSimulation.Structuring.Sectioning;

namespace ParticleSimulation.Structuring
{
    public interface ICore
    {
        IEnumerable<IParticleSection> Sections { get;}
        void AddSection(IParticleSection particleSection);

    }
}