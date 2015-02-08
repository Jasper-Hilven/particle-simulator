using ParticleSimulation.Initialization;
using ParticleSimulation.Structuring;
using ParticleSimulation.Structuring.Sectioning;
using ParticleSimulation.Tactics.KMeans.KMeansSectioning;
using SharpDX;

namespace ParticleSimulation.Tactics.GNeighbour
{
    public class GNeighbourSectionFactory:ISectionFactory
    {
        private KMeansSectionFactory decoFactory;

        public GNeighbourSectionFactory(float maxDifference)
        {
            decoFactory = new KMeansSectionFactory(0,0,maxDifference);
        }

        public IParticleSection GetSection(Vector3 lowerBound, Vector3 upperBound, ICore core, SectionGrid grid)
        {
            var particleSection = (KMeansSection)decoFactory.GetSection(lowerBound, upperBound, core, grid);
            particleSection.CurrentPosition = (lowerBound + upperBound)/2;
            return particleSection;
        }
    }
}