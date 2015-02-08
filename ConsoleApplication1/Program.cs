using System;
using System.Text;

namespace ParticleSimulation
{
    
    class Program
    {
        static void Main(string[] args)
        {
            var buf = new StringBuilder();
            new SimulatorRunner().RunAll();
         /*   for (int i = 0; i < 30; i++)
            {
                long a = 49900000;
                buf.AppendLine(i + " & 49900000 & " + (a * i) + " & " + "  \\\\ \\hline");
                
            }
            System.IO.File.WriteAllText(@"C:\Users\Jasper\Desktop\writeOut.txt", buf.ToString());
            
        */
        }
    }
}
