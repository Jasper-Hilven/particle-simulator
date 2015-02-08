using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using ParticleSimulation.Settings;
using ParticleSimulation.Settings.HelperSettings;

namespace ParticleSimulation
{
    public class SimulatorRunner
    {
        public void RunAll()
        {
            var settingsFactory = new SettingsFactory();
            var threadList = new List<Thread>();
            const int threadsAtTheSameTime = 5;
            var position = 0;
            bool singleThreaded = false;
            if (singleThreaded)
            {
                foreach (var initializationParameterse in settingsFactory.GetNextSettings())
                {
                    var sim1 = new ParticleSimulator();
                    sim1.Simulate(initializationParameterse);

                }

                return;
            }

            foreach (var a in settingsFactory.GetNextSettings()
                .Select(setting1 => new Thread(() =>
                {
                    var k = position;
                    if (k >= threadsAtTheSameTime)
                        threadList[k - threadsAtTheSameTime].Join();
                    Console.Out.WriteLine("started new setting.");

                    var simulator1 = new ParticleSimulator();
                    simulator1.Simulate(setting1);
                })))
            {
                position++;
                Console.Out.WriteLine("initiated new setting.");
                threadList.Add(a);
                a.Start();
            }



        }
    }
}