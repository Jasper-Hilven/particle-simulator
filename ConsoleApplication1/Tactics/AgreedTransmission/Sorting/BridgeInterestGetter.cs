using System;
using ParticleSimulation.Structuring;
using System.Linq;
using ParticleSimulation.Structuring.Sectioning;

namespace ParticleSimulation.Tactics.AgreedTransmission.Sorting
{
    public class BridgeInterestGetter
    {
        Tuple<Point3, int>[] Directions = new Tuple<Point3, int>[6] { 
            new Tuple<Point3, int>(new Point3(-1, 0, 0), 0), new Tuple<Point3, int>(new Point3(1, 0, 0), 1), 
            new Tuple<Point3, int>(new Point3(0,-1,  0), 2), new Tuple<Point3, int>(new Point3(0,1, 0), 3), 
            new Tuple<Point3, int>(new Point3(0,0,-1), 4), new Tuple<Point3, int>(new Point3(0,0,1), 5), };
        public void GetOldNeighbourInterests(SimulationStructure simulationStructure)
        {
            var coreGrid = simulationStructure.CoreGrid;
            foreach (FtSection section in coreGrid.GetCores().SelectMany(c => c.Sections))
            {
                foreach (var direction in Directions)
                {
                    GetOldNeighbourInterest(direction.Item1, direction.Item2, section);
                }
                for (var i = 0; i < 6; i++)
                {
                    section.OwnIncreases[i] = (section.MaximumParticles - section.CurrentParticles.Count)/2 ;
                }
            }

        }
        private void GetOldNeighbourInterest(Point3 Direction, int positionDirection, FtSection section)
        {
            Point3 ownPosition;
            section.Grid.SectionCoreMapping.TryGetValue(section, out ownPosition);
            IParticleSection otherSection;
            section.Grid.SectionCoreMapping.TryGetKey(ownPosition + Direction, out otherSection);
            if (otherSection == null)
            {
                section.NeighbourCanReceive[positionDirection] = 0;
                return;
            }
            var bcsec = ((FtSection)otherSection);
            //Needs to be old because of the message.
            section.NeighbourCanReceive[positionDirection] = bcsec.OwnChanges[twoOpposite(positionDirection)] + bcsec.OwnIncreases[twoOpposite(positionDirection)];

            bcsec.OwnChanges[twoOpposite(positionDirection)] = bcsec.NewOwnChanges[twoOpposite(positionDirection)];

        }
        private int twoOpposite(int b)
        {
            var j = b % 2;
            return (j == 0) ? b + 1 : b-1;
        }
    }
}