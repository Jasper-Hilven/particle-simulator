using System;
using System.Collections.Generic;
using System.Linq;
using ParticleSimulation.CostCalculation;
using ParticleSimulation.CostCalculation.CostStorage;
using ParticleSimulation.FieldCalculation.Cost.Types;
using ParticleSimulation.Structuring;
using ParticleSimulation.Structuring.Sectioning;

namespace ParticleSimulation.FieldCalculation.Cost
{
    /// <summary>
    ///     Ignores particleReductionFactor
    /// </summary>
    public class CohesionComCalculator : IFieldComCostCalculator
    {
        private const double CostSendingPerCellCorner = 4.08044386523 * 0.001 * 0.001 * 0.001d;
        private const double CostReceivingPerCellCorner = 4.08044386523 * 0.001 * 0.001 * 0.001d;
        private readonly int cellsPerSectionPerDim;
        private readonly ICostType receiveCost = ParticleWpcCoresSectionCoreRole.GetParticleWpcCoresReceiveCost();
        private readonly ICostType sendCost = ParticleWPCCoresHomeCoreRole.GetParticleWpcCoresSend();
        private readonly ICostStorage storage;

        private ICore currentCore;

        private Dictionary<ICore, Dictionary<ISection, HashSet<Point3>>> receiveCores =
            new Dictionary<ICore, Dictionary<ISection, HashSet<Point3>>>();

        public CohesionComCalculator(ICostStorage storage, int cellsPerSectionPerDim)
        {
            this.storage = storage;
            this.cellsPerSectionPerDim = cellsPerSectionPerDim;
        }

        public void AddParticleCost(ICore homeCore, ICore sectionCore, Particle particle, int step)
        {
            if (currentCore != homeCore)
                SendAllCosts(homeCore, step);
            var receiveSection = getContainingSection(sectionCore, particle);
            if (receiveSection == null)
            {
                // ReSharper disable once LocalizableElement
                // Console.WriteLine("warning: missed particle in communicationcostcalculation");
                return;
            }
            var positionInSection = GetPositionInSection(receiveSection, particle, cellsPerSectionPerDim);

            AddCells(positionInSection, sectionCore, receiveSection);
        }

        public void FlushCommunicationCosts(int step)
        {
            SendAllCosts(null, step);
        }

        private Point3 GetPositionInSection(ISection section, Particle particle, int i)
        {
            var xRel =
                (int)(cellsPerSectionPerDim * (particle.Position.X - section.LowerBound.X) / (section.UpperBound.X - section.LowerBound.X) * i);
            var yRel =
                (int)(cellsPerSectionPerDim * (particle.Position.Y - section.LowerBound.Y) / (section.UpperBound.Y - section.LowerBound.Y) * i);
            var zRel =
                (int)(cellsPerSectionPerDim * (particle.Position.Z - section.LowerBound.Z) / (section.UpperBound.Z - section.LowerBound.Z) * i);
            return new Point3(xRel, yRel,  zRel);
        }

        private ISection getContainingSection(ICore sectionCore, Particle particle)
        {
            return sectionCore.Sections.FirstOrDefault(particleSection => particleSection.Contains(particle.Position));
        }

        private void AddCells(Point3 position, ICore sectionCore, ISection section)
        {
            if (!receiveCores.ContainsKey(sectionCore))
                receiveCores.Add(sectionCore, new Dictionary<ISection, HashSet<Point3>>());
            if (!receiveCores[sectionCore].ContainsKey(section))
                receiveCores[sectionCore].Add(section, new HashSet<Point3>());
            var set = receiveCores[sectionCore][section];
            set.Add(position);
            set.Add(position + new Point3(0, 0, 1));
            set.Add(position + new Point3(0, 1, 0));
            set.Add(position + new Point3(0, 1, 1));
            set.Add(position + new Point3(1, 0, 0));
            set.Add(position + new Point3(1, 0, 1));
            set.Add(position + new Point3(1, 1, 0));
            set.Add(position + new Point3(1, 1, 1));
        }

        private void SendAllCosts(ICore newCore, int step)
        {
            foreach (var receiveCore in receiveCores)
            {
                
                
                var singleReceiveCoreCosts = receiveCore.Value;
                foreach (var singleReceiveCoreCost in singleReceiveCoreCosts)
                {
                    var singlePointSet = singleReceiveCoreCost.Value;
                    var receiveStoreCost = singlePointSet.Count * CostReceivingPerCellCorner;
                    var sendStorecost = singlePointSet.Count * CostSendingPerCellCorner;
                   // Console.Out.WriteLine(receiveStoreCost);
                    storage.AddCost(sendCost, currentCore, step, sendStorecost);
                    storage.AddCost(receiveCost, receiveCore.Key, step, receiveStoreCost);    
                }
                
            }
            receiveCores = new Dictionary<ICore, Dictionary<ISection, HashSet<Point3>>>();
            currentCore = newCore;
        }
    }
}