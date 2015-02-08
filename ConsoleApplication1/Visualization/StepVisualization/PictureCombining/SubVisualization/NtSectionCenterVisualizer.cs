using System;
using System.Drawing;
using System.Linq;
using Emgu.CV;
using Emgu.CV.Structure;
using ParticleSimulation.Sorting;
using ParticleSimulation.Structuring;

namespace ParticleSimulation.Visualization.StepVisualization.PictureCombining.SubVisualization
{
    public class NtSectionCenterVisualizer:ISubVisualizer
    {
        private IPictureCoordinateConverter visualizer;
        public NtSectionCenterVisualizer(IPictureCoordinateConverter visualizer)
        {
            this.visualizer = visualizer;
        }

        public void DrawVisualization(SimulationStructure structure, Image<Rgb, byte> image)
        {
            drawCoreCenters(structure,image);
        }
        private void drawCoreCenters(SimulationStructure structure, Image<Rgb, byte> image)
        {
            foreach (var section in structure.CoreGrid.GetCores().SelectMany(c => c.Sections))
            {
                if (!(section is NeighbourTransferSection))
                    break;
                var curpos = ((NeighbourTransferSection)section).CurrentPosition;
                var coord = getPictureCoordinatesFor(curpos.X, curpos.Z, structure);

                for (var i = -1; i < 2; i++)
                {
                    for (var j = -1; j < 2; j++)
                    {
                       try
                        {
                            image[coord.Item2 + i, coord.Item1 + j] = new Rgb(Color.Red);
                        }catch
                        {
                        }
                    }
                }

            }
        }
        private Tuple<int, int> getPictureCoordinatesFor(float f, float f1, SimulationStructure structure)
        {
            return visualizer.GetPictureCoordinatesFor(f, f1, structure);
        }
    }
}