using System.Drawing.Printing;
using System.Runtime.InteropServices;
using SharpDX;

namespace ParticleSimulation.Structuring
{
    [StructLayout( LayoutKind.Sequential,Pack=1)]
    public struct Particle
    {
        public Vector3 Position { get; set; }
        public Vector3 Velocity { get; set; }        
        public bool IsNull;
        public static Particle Null { get { return new Particle() { IsNull = true }; } }
    }
}