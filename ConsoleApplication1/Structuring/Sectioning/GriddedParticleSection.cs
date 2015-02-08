using System;
using System.Collections.Generic;
using SharpDX;

namespace ParticleSimulation.Structuring.Sectioning
{
    public abstract class GriddedParticleSection : Section, IParticleSection
    {
        public readonly SectionGrid Grid;

        protected GriddedParticleSection(SectionGrid grid, ICore core)
        {
            Grid = grid;
            Core = core;
        }



        public IParticleSection GetContainingParticle(Vector3 pos)
        {
            if (Contains(pos))
                return this;
            var divVector = new Vector3(UpperBound.X - LowerBound.X, UpperBound.Y - LowerBound.Y, UpperBound.Z - LowerBound.Z);
            var corPos = pos - LowerBound;
            var difPoint = new Point3((int)Math.Floor(corPos.X / divVector.X), (int)Math.Floor(corPos.Y / divVector.Y), (int)Math.Floor(corPos.Z / divVector.Z));
            Point3 myPoint;
            IParticleSection otherParticleSection;
            Grid.SectionCoreMapping.TryGetValue(this, out myPoint);


            if (!Grid.SectionCoreMapping.TryGetKey(myPoint + difPoint, out otherParticleSection))
                return this;
            //THIS IS A QUICKFIX to avoid floating point rounding errors. TODO make this work correctly


            return otherParticleSection;
        }

        public abstract IEnumerable<Particle> Particles { get; }

        public abstract void AddParticle(Particle particle);
        public abstract void ApplyToAllParticles(ApplyToParticleDelegate applyTo);

          public ICore Core { get; private set; }
    }
}