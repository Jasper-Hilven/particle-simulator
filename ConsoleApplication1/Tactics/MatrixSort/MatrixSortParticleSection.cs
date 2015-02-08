using System.Collections.Generic;
using ParticleSimulation.Structuring;
using ParticleSimulation.Structuring.Sectioning;

namespace ParticleSimulation.Tactics.MatrixSort
{
    public class MatrixSortParticleSection : GriddedParticleSection
    {
        public Particle[, ,] ParticlesMatrix { get; private set; }
        
        public readonly int XSize ;
        public readonly int YSize ;
        public readonly int ZSize ;
        private Point3 fillCounter;
        public MatrixSortParticleSection(ICore core, SectionGrid grid,int cellSizeSingleDim):base(grid,core)
        {
            XSize = cellSizeSingleDim;
            YSize = cellSizeSingleDim;
            ZSize = cellSizeSingleDim;
            ParticlesMatrix = new Particle[XSize, YSize, ZSize];
            fillCounter = new Point3();
        }

        
        public override IEnumerable<Particle> Particles
        {
            get
            {
                for (var i = 0; i < XSize; i++)
                {
                    for (var j = 0; j < YSize; j++)
                    {
                        for (var k = 0; k < ZSize; k++)
                        {
                            if (!ParticlesMatrix[i, j, k].IsNull)
                                yield return ParticlesMatrix[i, j, k];
                        }
                    }
                }
            }
        }
        public override void AddParticle(Particle particle)
        {
                ParticlesMatrix[fillCounter.X, fillCounter.Y, fillCounter.Z] = particle;
            fillCounter += new Point3(1, 0, 0);
            if (fillCounter.X >= XSize)
                fillCounter += new Point3(-XSize, 1, 0);
            if (fillCounter.Y >= YSize)
                fillCounter += new Point3(0, -YSize, 1);
        }
        public override void ApplyToAllParticles(ApplyToParticleDelegate applyTo)
        {
            for (var i = 0; i < XSize; i++)
            {
                for (var j = 0; j < YSize; j++)
                {
                    for (var k = 0; k < ZSize; k++)
                    {
                        ParticlesMatrix[i, j, k] = applyTo(ParticlesMatrix[i, j, k]);
                    }
                }
            }
        }
    }
}