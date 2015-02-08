using System;
using System.Collections.Generic;
using ParticleSimulation.FieldCalculation.Cost.Types;
using ParticleSimulation.Moving;
using ParticleSimulation.Sorting.Cost;
using ParticleSimulation.Structuring;
using ParticleSimulation.Structuring.Sectioning;

namespace ParticleSimulation.CostCalculation.CostReview
{
    public class MaxStepCostCalculator : ICostReviewer
    {
        private readonly ICostReviewer decoReviewer;

        public MaxStepCostCalculator(ICostReviewer decoReviewer)
        {
            this.decoReviewer = decoReviewer;
        }



        public Dictionary<ICore, Dictionary<ICostType, double>> GetCost(int step)
        {
            var maxComCost = 0d;
            var maxSortCost = 0d;
            var maxPartCost = 0d;
            var maxConstCost = 0d;
            foreach (var corecostp in decoReviewer.GetCost(step))
            {
                var curComCost = 0d;
                var curSortCost = 0d;
                var curPartCost = 0d;
                var curConstCost = 0d;
                foreach (var d in corecostp.Value)
                {
                    if (d.Key is SortingCostReceiveParticle || d.Key is SortingCostSendParticle ||
                        d.Key is SortingCostParticleSwap || d.Key is SortingCostParticleComparison)
                    { curSortCost += d.Value; continue; }
                    if (d.Key is ParticleWpcCoresSectionCoreRole || d.Key is ParticleWPCCoresHomeCoreRole)
                    {
                        curComCost += d.Value;
                        continue;
                    }
                    if (d.Key is ParticleMovementCost)
                    {
                        curPartCost += d.Value;
                        continue;

                    }
                    if (d.Key is ConstantSolvingCost)
                    {
                        curConstCost += d.Value;
                        continue;
                    }
                    throw new Exception("Strange type of cost occurred");
                }
                maxComCost = Math.Max(curComCost, maxComCost);
                maxSortCost = Math.Max(curSortCost, maxSortCost);
                maxPartCost = Math.Max(curPartCost, maxPartCost);
                maxConstCost = Math.Max(curConstCost, maxConstCost);
            }
            var retDic = new Dictionary<ICore, Dictionary<ICostType, double>>();
            var dummyCore = new DummyCore();
            retDic.Add(dummyCore, new Dictionary<ICostType, double>());
            retDic[dummyCore].Add(new SortingCostParticleComparison(), maxSortCost);
            retDic[dummyCore].Add(ParticleWpcCoresSectionCoreRole.GetParticleWpcCoresReceiveCost(), maxComCost);
            retDic[dummyCore].Add(ConstantSolvingCost.GetConstantSolvingCost(), maxConstCost);
            retDic[dummyCore].Add(ParticleMovementCost.GetParticleMovementCostType(), maxPartCost);
            return retDic;
        }
    }
    public class DummyCore : ICore
    {
        public DummyCore()
        {
            Sections = null;
        }

        public IEnumerable<IParticleSection> Sections { get; private set; }
        public void AddSection(IParticleSection particleSection)
        {
            throw new NotImplementedException();
        }
    }

}
