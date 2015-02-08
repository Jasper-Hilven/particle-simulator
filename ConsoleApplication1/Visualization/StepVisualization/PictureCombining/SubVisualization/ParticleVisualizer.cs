using System;
using System.Drawing;
using System.Linq;
using Emgu.CV;
using Emgu.CV.Structure;
using ParticleSimulation.Structuring;
using ParticleSimulation.Structuring.Sectioning;

namespace ParticleSimulation.Visualization.StepVisualization.PictureCombining.SubVisualization
{
    public class ParticleVisualizer:ISubVisualizer
    {
        private IPictureCoordinateConverter visualizer;

        public ParticleVisualizer(IPictureCoordinateConverter visualizer)
        {
            this.visualizer = visualizer;
        }

        public void DrawVisualization(SimulationStructure structure, Image<Rgb, byte> image)
        {
            drawParticles(structure,image);
        }

        private void drawParticles(SimulationStructure structure, Image<Rgb, byte> image)
        {
            var sections = structure.CoreGrid.GetCores().SelectMany(o => o.Sections);
            foreach (var sec in sections)
            {
                var color = GetColorOf(sec, structure);
                foreach (var particle in sec.Particles)
                {
                    var coord = getPictureCoordinatesFor(particle.Position.X, particle.Position.Z, structure);
                    try
                    {
                        image[coord.Item2, coord.Item1] = color;
                    }
                    catch (Exception)
                    {
                        
                    }
                }
            }
        }

        private Tuple<int, int> getPictureCoordinatesFor(float f, float f1, SimulationStructure structure)
        {
            return visualizer.GetPictureCoordinatesFor(f, f1, structure);
        }

        private Rgb GetColorOf(IParticleSection particleSection, SimulationStructure structure)
        {
            var coord = getPictureCoordinatesFor(particleSection.UpperBound.X, particleSection.UpperBound.Z, structure);
            var r = (0.5 + 0.4 * Math.Cos((double)coord.Item1 * 20 + coord.Item2 * 22)) * 256;
            var g = (0.5 + 0.4 * Math.Cos((double)coord.Item1 * 18 + coord.Item2 * 20)) * 256;
            var b = (0.5 + 0.4 * Math.Cos((double)coord.Item1 * 16 + coord.Item2 * 18)) * 256;


            return new Rgb(Color.FromArgb((int)r, (int)g, (int)b));
        }

    }
}