using System.Collections.Generic;

namespace ParticleSimulation.Structuring.Sectioning
{
    /// <summary>
    /// DEPRICATED
    /// </summary>
    public class SectionGrid
    {

        public SectionGrid()
        {
            SectionCoreMapping = new DDictionary<IParticleSection, Point3>(new Dictionary<IParticleSection, Point3>(), new Dictionary<Point3,IParticleSection>());
        }
        public DDictionary<IParticleSection, Point3> SectionCoreMapping { get; private set; }

    }
}