using System.Collections.Generic;
using ParticleSimulation.Structuring.Sectioning;

namespace ParticleSimulation.Structuring
{
    public class GridDistribution
    {
        public DDictionary<ICore, IParticleSection> Grids { get; private set; }

        public GridDistribution()
        {
            Grids = new DDictionary<ICore, IParticleSection>(new Dictionary<ICore, IParticleSection>(), new Dictionary<IParticleSection, ICore>());
        }

    }

}

