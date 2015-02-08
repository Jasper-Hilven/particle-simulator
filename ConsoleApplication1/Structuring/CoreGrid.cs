using System;
using System.Collections.Generic;

namespace ParticleSimulation.Structuring
{
    public class CoreGrid
    {
        public readonly DDictionary<ICore,Point3> Positions = new DDictionary<ICore, Point3>(new Dictionary<ICore, Point3>(),new Dictionary<Point3, ICore>() );
        public void AddCore(ICore core,Point3 position)
        {
            Positions.Add(core,position);
        }
        public void RemoveCore(ICore core,Point3 position)
        {
            Positions.Remove(core);
        }
        public IEnumerable<ICore> GetCores()
        {
            return Positions.Keys;
        }
        public Point3 GetPositionOf(ICore core)
        {
            Point3 outPoint;
            if(!Positions.TryGetValue(core,out outPoint))
                throw new Exception("Argument invalid");
            return outPoint;

        }

        public ICore GetCoreAt(Point3 position)
        {
            ICore outCore;
            Positions.TryGetKey(position, out outCore);
            return outCore;
        }


        public double GetDistanceBetween(ICore core1,ICore core2)
        {
            Point3 pos1;
            Point3 pos2;
            Positions.TryGetValue(core1, out pos1);
            Positions.TryGetValue(core2, out pos2);
            return (pos2 - pos1).ToVector3().Length();
        }
        
    }
}