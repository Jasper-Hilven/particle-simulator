using System;
using System.Collections.Generic;
using System.Linq;
using ParticleSimulation.Structuring;
using ParticleSimulation.Structuring.Sectioning;

namespace ParticleSimulation.Tactics.SideSort
{
    public class SideSorterTransmitter
    {
        private bool setNoNeighbourAdvantage = false;

        public void TransmitParticles(SimulationStructure simulationStructure, int step)
        {
            if (!setNoNeighbourAdvantage)
            {
                InitiateCornerCases(simulationStructure);
                setNoNeighbourAdvantage = true;
            }
            foreach (
                var section in
                    simulationStructure.CoreGrid.GetCores().SelectMany(core => core.Sections.Cast<ParticleSection>()))
            {
                Point3 p;
                IParticleSection otherParticleSection;
                section.Grid.SectionCoreMapping.TryGetValue(section, out p);
                //checkForNoDoubleSending(section);
                if (section.Grid.SectionCoreMapping.TryGetKey(p + new Point3(1, 0, 0), out otherParticleSection))
                    SwapXBorderSection(section, ((ParticleSection)otherParticleSection), step,
                        simulationStructure);
                if (section.Grid.SectionCoreMapping.TryGetKey(p + new Point3(0, 1, 0), out otherParticleSection))
                    SwapYBorderSection(section, ((ParticleSection)otherParticleSection), step,
                        simulationStructure);
                if (section.Grid.SectionCoreMapping.TryGetKey(p + new Point3(0, 0, 1), out otherParticleSection))
                    SwapZBorderSection(section, ((ParticleSection)otherParticleSection), step,
                        simulationStructure);
            }
        }

        private static void InitiateCornerCases(SimulationStructure simulationStructure)
        {
            foreach (
                var section in
                    simulationStructure.CoreGrid.GetCores().SelectMany(core => core.Sections.Cast<ParticleSection>()))
            {
                Point3 p;
                IParticleSection otherParticleSection;
                section.Grid.SectionCoreMapping.TryGetValue(section, out p);
                if (!section.Grid.SectionCoreMapping.TryGetKey(p + new Point3(1, 0, 0), out otherParticleSection))
                    section.elementsPerSide[1] = 1;
                if (!section.Grid.SectionCoreMapping.TryGetKey(p + new Point3(0, 1, 0), out otherParticleSection))
                    section.elementsPerSide[3] = 1;
                if (!section.Grid.SectionCoreMapping.TryGetKey(p + new Point3(0, 0, 1), out otherParticleSection))
                    section.elementsPerSide[5] = 1;
                if (!section.Grid.SectionCoreMapping.TryGetKey(p + new Point3(-1, 0, 0), out otherParticleSection))
                    section.elementsPerSide[0] = 1;
                if (!section.Grid.SectionCoreMapping.TryGetKey(p + new Point3(0, -1, 0), out otherParticleSection))
                    section.elementsPerSide[2] = 1;
                if (!section.Grid.SectionCoreMapping.TryGetKey(p + new Point3(0, 0, -1), out otherParticleSection))
                    section.elementsPerSide[4] = 1;
            }
        }

        private void checkForNoDoubleSending(ParticleSection section)
        {
            var testSet = new HashSet<int>();
            foreach (var reffedParticle in section.sendParticles.SelectMany(o => o))
            {
                if (!testSet.Add(reffedParticle.reference))
                    throw new Exception("Double sending particles!");
            }
        }

        private void SwapZBorderSection(ParticleSection section, ParticleSection upperParticleSection, int step,
            SimulationStructure simulationStructure)
        {
            const int mySectionConst = 5;
            const int otherSectionConst = 4;
            int i = section.sendParticles[mySectionConst].Count - 1;
            int j = upperParticleSection.sendParticles[otherSectionConst].Count - 1;
            int goodSwaps = 0;
            while (i >= 0 && j >= 0 && section.sendParticles[mySectionConst][i].particle.Position.Z > upperParticleSection.sendParticles[otherSectionConst][j].particle.Position.Z)
            {
                goodSwaps++;
                i--;
                j--;
            }
            SwapBetterList(section, upperParticleSection, goodSwaps, mySectionConst, i, otherSectionConst, j);
        }

        private void SwapYBorderSection(ParticleSection section, ParticleSection upperParticleSection, int step,
            SimulationStructure simulationStructure)
        {
            const int mySectionConst = 3;
            const int otherSectionConst = 2;
            int i = section.sendParticles[mySectionConst].Count - 1;
            int j = upperParticleSection.sendParticles[otherSectionConst].Count - 1;
            int goodSwaps = 0;
            while (i >= 0 && j >= 0 && section.sendParticles[mySectionConst][i].particle.Position.Y > upperParticleSection.sendParticles[otherSectionConst][j].particle.Position.Y)
            {
                goodSwaps++;
                i--;
                j--;
            }
            SwapBetterList(section, upperParticleSection, goodSwaps, mySectionConst, i, otherSectionConst, j);
        }

        private void SwapXBorderSection(ParticleSection section, ParticleSection upperParticleSection, int step,
            SimulationStructure simulationStructure)
        {
            const int mySectionConst = 1;
            const int otherSectionConst = 0;
            int i = section.sendParticles[mySectionConst].Count - 1;
            int j = upperParticleSection.sendParticles[0].Count - 1;
            int goodSwaps = 0;
            while (i >= 0 && j >= 0 && section.sendParticles[mySectionConst][i].particle.Position.X > upperParticleSection.sendParticles[otherSectionConst][j].particle.Position.X)
            {
                goodSwaps++;
                i--;
                j--;
            }
            SwapBetterList(section, upperParticleSection, goodSwaps, mySectionConst, i, otherSectionConst, j);
        }

        private static void SwapBetterList(ParticleSection section, ParticleSection upperParticleSection, int goodSwaps,
            int mySectionConst, int i, int otherSectionConst, int j)
        {
            for (int k = 0; k < goodSwaps; k++)
            {
                if (section.sendParticles[mySectionConst][i + 1 + k].particle.IsNull || upperParticleSection.sendParticles[otherSectionConst][j + 1 + k].particle.IsNull)
                    throw new Exception("dummy particle got in.");
                section.BulkParticles[section.sendParticles[mySectionConst][i + 1 + k].reference] =
                    upperParticleSection.sendParticles[otherSectionConst][j + 1 + k].particle;
                upperParticleSection.BulkParticles[upperParticleSection.sendParticles[otherSectionConst][j + 1 + k].reference] =
                    section.sendParticles[mySectionConst][i + 1 + k].particle;
            }
            UpdateElementsPerSection(section, upperParticleSection, mySectionConst, otherSectionConst, goodSwaps);
        }

        private static void UpdateElementsPerSection(ParticleSection downSection, ParticleSection otherSection, int myDirection, int otherDirection, int goodSwaps)
        {

            var total = downSection.sendParticles[myDirection].Count;
            var updated = total;
            if (total == goodSwaps) ////NEED MORE SPACE
                updated = Math.Min(total * 2, downSection.BulkParticles.Count / 7);
            if (3 * goodSwaps <= 2 * total)//// LESS SPACE NEEDED
                updated = Math.Max(1, (total) / 2);
            otherSection.elementsPerSide[otherDirection] = updated;
            downSection.elementsPerSide[myDirection] = updated;
        }
    }
}