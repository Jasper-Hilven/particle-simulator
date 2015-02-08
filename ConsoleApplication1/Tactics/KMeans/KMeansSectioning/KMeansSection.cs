using System;
using System.Collections.Generic;
using System.Linq;
using ParticleSimulation.Sorting;
using ParticleSimulation.Structuring;
using ParticleSimulation.Structuring.Sectioning;
using SharpDX;

namespace ParticleSimulation.Tactics.KMeans.KMeansSectioning
{
    public class KMeansSection : NeighbourTransferSection
    {


        //A number between 0 and 1 that represents how many of the particles are closer to the 

        public int[] neighbourSizes = new int[6];
        
        public new List<Particle> CurrentParticles { get { return base.CurrentParticles; }
            set
            {
                LastSize = base.CurrentParticles.Count;
                base.CurrentParticles= value;
            }
        }
    
        public int LastSize { get; private set; }


        public KMeansSection(SectionGrid grid, ICore core)
            : base(grid, core)
        {}


    }
}
