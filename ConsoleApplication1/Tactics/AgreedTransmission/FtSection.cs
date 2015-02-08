using System.Collections.Generic;
using ParticleSimulation.Sorting;
using ParticleSimulation.Structuring;
using ParticleSimulation.Structuring.Sectioning;
using SharpDX;

namespace ParticleSimulation.Tactics.AgreedTransmission
{
    public class FtSection : NeighbourTransferSection
    {
         
        public FtSection(SectionGrid grid, ICore core) : base(grid, core)
        {
            NeighbourCanReceive = new int[6];
            OwnChanges = new int[6];
            OwnIncreases = new int[6];
            NewOwnChanges = new int[6];
        }

        public int[] NeighbourCanReceive;
        public int[] OwnIncreases;
        public int[] OwnChanges;
        public int[] NewOwnChanges;
        public int MaximumParticles;

    }
}