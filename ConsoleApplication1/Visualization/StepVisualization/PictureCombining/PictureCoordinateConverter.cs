using System;
using System.Drawing;
using System.Linq;
using ParticleSimulation.Structuring;
using SharpDX;

namespace ParticleSimulation.Visualization.StepVisualization.PictureCombining
{
    class PictureCoordinateConverter : IPictureCoordinateConverter
    {
        private bool calculatedLatestBound;
        private Vector3 latestBound;
        private  int height; //Z coordinate
        private int width; //X coordinate
        public PictureCoordinateConverter(Point size)
        {
            height = size.Y;
            width = size.X;
        }

        public Tuple<int, int> GetPictureCoordinatesFor(float x, float z, SimulationStructure structure)
        {
            latestBound = LatestBoundary(structure);
            var xFac = x / latestBound.X;
            var zFac = z / latestBound.Z;
            xFac *= (width - 2);
            zFac *= (height - 2);
            return new Tuple<int, int>(((int)xFac), ((int)zFac));

        }

        public void SetParticleWindowSize(Point size)
        {
            height = size.Y;
            width = size.X;
        }

        private Vector3 LatestBoundary(SimulationStructure structure)
        {
            if (calculatedLatestBound)
            {
                return latestBound;
            }
            var maxPoint = structure.CoreGrid.Positions.Values.Aggregate(new Point3(0, 0, 0),
                (a, b) =>
                    (a.ToVector3().LengthSquared() >
                     b.ToVector3().LengthSquared())
                        ? a
                        : b);
            var maxCore = structure.CoreGrid.GetCoreAt(maxPoint);
            latestBound =
                maxCore.Sections.Aggregate(
                    (a, b) => (a.UpperBound.LengthSquared() > b.UpperBound.LengthSquared()) ? a : b).UpperBound;
            calculatedLatestBound = true;
            return latestBound;
        }
    }
}