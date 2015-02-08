using System.Collections.Generic;
using ParticleSimulation.Structuring;
using ParticleSimulation.Structuring.Sectioning;
using ParticleSimulation.Tactics.OuterList;
using SharpDX;

namespace ParticleSimulation.Initialization
{
    public class CoreSectionBuilder
    {
        public void CreateCoresAndSections(ICoreFactory coreFactory, ISectionFactory sectionFactory,out CoreGrid coreGrid, out IParticleSection overParticleSection, Point3 numberOfCores, Vector3 SectionSize, Point3 numberOfSectionPerCore)
        {
            overParticleSection = new StrictBoundaryParticleSection(null,null) { LowerBound = new Vector3(), UpperBound = SectionSize };
            var xCores = numberOfCores.X;
            var yCores = numberOfCores.Y;
            var zCores = numberOfCores.Z;
            var xSections = numberOfSectionPerCore.X;
            var ySections = numberOfSectionPerCore.Y;
            var zSections = numberOfSectionPerCore.Z;
            coreGrid = new CoreGrid();
            var sectionGrid = new SectionGrid();
            for (var i = 0; i < xCores; i++)
            {
                for (var j = 0; j < yCores; j++)
                {
                    for (var k = 0; k < zCores; k++)
                    {

                        GenerateSingleCore(coreFactory, sectionFactory, coreGrid, overParticleSection, ySections, xSections, sectionGrid, zCores, k, yCores, j, xCores, i, zSections);
                    }
                }
            }



        }

        private static void GenerateSingleCore(ICoreFactory coreFactory, ISectionFactory sectionFactory, CoreGrid coreGrid,
                                               IParticleSection overParticleSection, int ySections, int xSections, SectionGrid sectionGrid,
                                               int zCores, int k, int yCores, int j, int xCores, int i, int zSections)
        {
            var sectionList = new List<IParticleSection>();
            var middleSection = createSubSection(overParticleSection, i, xCores, j, yCores, k, zCores, null, null, new dummyFactory());
            var core = coreFactory.GetCore();

            for (var l = 0; l < xSections; l++)
            {
                for (var m = 0; m < ySections; m++)
                {
                    for (var n = 0; n < zSections; n++)
                    {
                        var innerSection = createSubSection(middleSection, l, xSections, m, ySections, n,
                                                            zSections, sectionGrid, core, sectionFactory);

                        sectionGrid.SectionCoreMapping.Add(innerSection,
                                                           new Point3(i*xSections + l, j*ySections + m, k*zSections + n));
                        sectionList.Add(innerSection);

                        core.AddSection(innerSection);
                    }
                }
            }
            coreGrid.AddCore(core, new Point3(i, j, k));
        }

        private static IParticleSection createSubSection(IParticleSection overParticleSection, int i, int xCores, int j, int yCores, int k, int zCores, SectionGrid grid, ICore core, ISectionFactory sectionFactory)
        {
            var LowerBound =
                new Vector3(overParticleSection.LowerBound.X +
                            i *
                            (overParticleSection.UpperBound.X -
                             overParticleSection.LowerBound.X) /
                            xCores,
                            overParticleSection.LowerBound.Y +
                            j *
                            (overParticleSection.UpperBound.Y -
                             overParticleSection.LowerBound.Y) /
                            yCores,
                            overParticleSection.LowerBound.Z +
                            k *
                            (overParticleSection.UpperBound.Z -
                             overParticleSection.LowerBound.Z) /
                            zCores
                    );
            var UpperBound = new Vector3(overParticleSection.LowerBound.X +
                                     (i + 1) *
                                     (overParticleSection.UpperBound.X -
                                      overParticleSection.LowerBound.X) / xCores,
                                     overParticleSection.LowerBound.Y +
                                     (j + 1) *
                                     (overParticleSection.UpperBound.Y -
                                      overParticleSection.LowerBound.Y) / yCores,
                                     overParticleSection.LowerBound.Z +
                                     (k + 1) *
                                     (overParticleSection.UpperBound.Z -
                                      overParticleSection.LowerBound.Z) / zCores
               );
            return sectionFactory.GetSection(LowerBound, UpperBound, core,grid);
        }
        private class dummyFactory : ISectionFactory
        {
            public IParticleSection GetSection(Vector3 lowerBound, Vector3 upperBound, ICore core,SectionGrid grid)
            {
                return new DummyParticleSection(){LowerBound = lowerBound,UpperBound = upperBound};
            }
        }

        private class DummyParticleSection : IParticleSection
        {
            public Vector3 LowerBound { get; set; }
            public Vector3 UpperBound { get; set; }
            public bool Contains(Vector3 pos)
            {
                throw new System.NotImplementedException();
            }

            public IParticleSection GetContainingParticle(Vector3 pos)
            {
                throw new System.NotImplementedException();
            }

              public void AddParticle(Particle particle)
            {
                throw new System.NotImplementedException();
            }

            public void ApplyToAllParticles(ApplyToParticleDelegate applyTo)
            {
                throw new System.NotImplementedException();
            }

            public void RemoveParticle(Particle particle)
            {
                throw new System.NotImplementedException();
            }

            public IEnumerable<Particle> Particles { get; private set; }
            public ICore Core { get; private set; }
        }
    }


    
}