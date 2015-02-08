using ParticleSimulation.Initialization;
using ParticleSimulation.Structuring;
using ParticleSimulation.Structuring.Sectioning;
using SharpDX;

namespace ParticleSimulation.Tactics.KMeans.KMeansSectioning
{
    public class KMeansSectionFactory:ISectionFactory
    {
        private readonly float sectionLocationStifness;
        private readonly float locationInertia;
        private float maximumDifferenceRadiusSquared;
        public KMeansSectionFactory(float sectionLocationStifness, float locationInertia, float maximumDifferenceRadius)
        {
            this.sectionLocationStifness = sectionLocationStifness;
            this.locationInertia = locationInertia;
            this.maximumDifferenceRadiusSquared = maximumDifferenceRadius * maximumDifferenceRadius;
        }

        public IParticleSection GetSection(Vector3 lowerBound, Vector3 upperBound, ICore core, SectionGrid grid)
        {
            return new KMeansSection(grid,core){LowerBound = lowerBound,UpperBound = upperBound,LocationStifness = sectionLocationStifness,LocationInertia = locationInertia,MaximumDifferenceRadiusSquared = maximumDifferenceRadiusSquared };
        }
    }
}