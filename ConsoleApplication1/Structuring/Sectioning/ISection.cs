using SharpDX;

namespace ParticleSimulation.Structuring.Sectioning
{
    public interface ISection
    {
        Vector3 LowerBound { get; set; }
        Vector3 UpperBound { get; set; }
        bool Contains(Vector3 pos);
    }
}