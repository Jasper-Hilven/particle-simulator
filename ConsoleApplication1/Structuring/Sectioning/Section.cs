using SharpDX;

namespace ParticleSimulation.Structuring.Sectioning
{
    public class Section:ISection
    {
        public Vector3 LowerBound { get; set; }
        public Vector3 UpperBound { get; set; }
        public bool Contains(Vector3 pos)
        {
            return (UpperBound.X > pos.X && UpperBound.Y > pos.Y && UpperBound.Z > pos.Z && LowerBound.X <= pos.X &&
                    LowerBound.Y <= pos.Y && LowerBound.Z <= pos.Z);
        }
    }
}