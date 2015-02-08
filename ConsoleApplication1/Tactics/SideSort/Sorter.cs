using System;
using System.Collections.Generic;
using ParticleSimulation.Sorting;
using ParticleSimulation.Structuring;
using ParticleSimulation.Structuring.Sectioning;
using SharpDX;

namespace ParticleSimulation.Tactics.SideSort
{
    public class Sorter : ISorter
    {
        //private int[] elementsPerSide;
        private readonly Random random = new Random(0);
        private readonly SideSorterTransmitter sideSorterTransmitter;

        public Sorter()
        {
            sideSorterTransmitter = new SideSorterTransmitter();
        }

        public void Sort(SimulationStructure simulationStructure, int step)
        {
            bool shown = false;
            foreach (IParticleSection particleSection in simulationStructure.GetAllSections())
            {
                SingleSectionSort((ParticleSection) particleSection);
                if (!shown)
                {
                    foreach(var s in ((ParticleSection)particleSection).elementsPerSide)
                    Console.Out.WriteLine(s);
                        shown = true;
                }
            }
            sideSorterTransmitter.TransmitParticles(simulationStructure, step);
        }


        private readonly bool[] full = new bool[6]; 
        private void SingleSectionSort(ParticleSection particleSection)
        {
            particleSection.sendParticles = new List<List<ParticleSection.ReffedParticle>>();
            for (int i = 0; i < 6; i++)
            {
                particleSection.sendParticles.Add(new List<ParticleSection.ReffedParticle>());
                full[i] = false;
            }
            var count = particleSection.BulkParticles.Count;
            var randNumb = random.Next(count);
            for (int i = randNumb; i < count; i++)
            {
                AddToSend0(particleSection.BulkParticles[i], i, particleSection);
            }
            for (int i = 0; i < randNumb; i++)
            {
                AddToSend0(particleSection.BulkParticles[i], i, particleSection);
            }
        }

        private void AddToSend0(Particle particle, int i, ParticleSection section)
        {
            var continueToNext = true;
            const int direction = 0;
            if (!full[direction] || particle.Position.X < section.sendParticles[direction][0].particle.Position.X)
            {
                var j = 1;
                var xPos = particle.Position.X;
                while (j < section.sendParticles[direction].Count &&
                       xPos < section.sendParticles[direction][j].particle.Position.X)
                    j++;
                ParticleSection.ReffedParticle outParticle;
                continueToNext = AddToRow(particle, i, section, direction, j, out outParticle);
                i = outParticle.reference;
                particle = outParticle.particle;
            }
            if (continueToNext)
                AddToSend1(particle, i, section);
        }

        private void AddToSend1(Particle particle, int i, ParticleSection section)
        {
            var continueToNext = true;
            const int direction = 1;
            if (!full[direction] || particle.Position.X > section.sendParticles[direction][0].particle.Position.X)
            {
                var j = 1;
                var xPos = particle.Position.X;
                while (j < section.sendParticles[direction].Count &&
                       xPos > section.sendParticles[direction][j].particle.Position.X)
                    j++;
                ParticleSection.ReffedParticle outParticle;
                continueToNext = AddToRow(particle, i, section, direction, j, out outParticle);
                i = outParticle.reference;
                particle = outParticle.particle;
            }
            if (continueToNext)
                AddToSend2(particle, i, section);
        }

        private void AddToSend2(Particle particle, int i, ParticleSection section)
        {
            var continueToNext = true;
            const int direction = 2;
            if (!full[direction] || particle.Position.Y < section.sendParticles[direction][0].particle.Position.Y)
            {
                var j = 1;
                var yPos = particle.Position.Y;
                while (j < section.sendParticles[direction].Count &&
                       yPos < section.sendParticles[direction][j].particle.Position.Y)
                    j++;
                ParticleSection.ReffedParticle outParticle;
                continueToNext = AddToRow(particle, i, section, direction, j, out outParticle);
                i = outParticle.reference;
                particle = outParticle.particle;
            }
            if (continueToNext)
                AddToSend3(particle, i, section);
        }

        private void AddToSend3(Particle particle, int i, ParticleSection section)
        {
            var continueToNext = true;
            const int direction = 3;
            if (!full[direction] || particle.Position.Y > section.sendParticles[direction][0].particle.Position.Y)
            {
                var j = 1;
                var yPos = particle.Position.Y;
                while (j < section.sendParticles[direction].Count &&
                       yPos > section.sendParticles[direction][j].particle.Position.Y)
                    j++;
                ParticleSection.ReffedParticle outParticle;
                continueToNext = AddToRow(particle, i, section, direction, j, out outParticle);
                i = outParticle.reference;
                particle = outParticle.particle;
            }
            if (continueToNext)
                AddToSend4(particle, i, section);
        }

        private void AddToSend4(Particle particle, int i, ParticleSection section)
        {
            var continueToNext = true;
            const int direction = 4;
            if (!full[direction] || particle.Position.Z < section.sendParticles[direction][0].particle.Position.Z)
            {
                var j = 1;
                var zPos = particle.Position.Z;
                while (j < section.sendParticles[direction].Count &&
                       zPos < section.sendParticles[direction][j].particle.Position.Z)
                    j++;
                ParticleSection.ReffedParticle outParticle;
                continueToNext = AddToRow(particle, i, section, direction, j, out outParticle);
                i = outParticle.reference;
                particle = outParticle.particle;
            }
            if (continueToNext)
                AddToSend5(particle, i, section);
        }

        private void AddToSend5(Particle particle, int i, ParticleSection section)
        {
            const int direction = 5;
            if (full[direction] && !(particle.Position.Z > section.sendParticles[direction][0].particle.Position.Z))
                return;
            var j = 1;
            var zPos = particle.Position.Z;
            while (j < section.sendParticles[direction].Count &&
                   zPos > section.sendParticles[direction][j].particle.Position.Z)
                j++;
            ParticleSection.ReffedParticle outParticle;
            AddToRow(particle, i, section, direction, j, out outParticle);
            //PARTICLE DROPS HERE
        }


        //Returns true if it drops a particle, false otherwise
        private bool AddToRow(Particle particle, int i, ParticleSection section, int direction, int j,
            out ParticleSection.ReffedParticle outParticle)
        {
            if (!full[direction])
            {
                section.sendParticles[direction].Add(new ParticleSection.ReffedParticle());
                for (int k = section.sendParticles[direction].Count - 1; k >= j; k--)
                {
                    section.sendParticles[direction][k] = section.sendParticles[direction][k - 1];
                }
              
                    
                section.sendParticles[direction][j-1] = new ParticleSection.ReffedParticle
                                                      {
                                                          particle = particle,
                                                          reference = i
                                                      };
                outParticle = ParticleSection.ReffedParticle.Null;
                if (section.sendParticles[direction].Count == section.elementsPerSide[direction])
                    full[direction] = true;
                
                return false;
            }
            outParticle = section.sendParticles[direction][0];
            for (int k = 0; k < j - 1; k++)
            {
                section.sendParticles[direction][k] = section.sendParticles[direction][k + 1];
            }
            section.sendParticles[direction][j - 1] = new ParticleSection.ReffedParticle
                                                      {
                                                          particle = particle,
                                                          reference = i
                                                      };

            return true;
        }

       

    }
}