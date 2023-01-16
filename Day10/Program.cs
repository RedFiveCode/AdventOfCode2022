using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Threading.Tasks;

namespace Day10
{
    internal class Program
    {
        static void Main(string[] args)
        {
            PartOne();
        }

        static void PartOne()
        {
            //var instructions = Reader.Read("Example.txt");
            //var instructions = Reader.Read("Example2.txt");
            var instructions = Reader.Read("Data.txt");

            var cyclesOfInterest = new List<int>()
            {
                20, 60, 100, 140, 180, 220
            };

            var processor = new InstructionProcessor();
            int cumulativeSignalStrength = 0;

            processor.CycleCompleted += (sender, registers) =>
            {
                if (cyclesOfInterest.Contains(registers.Cycles))
                {
                    cumulativeSignalStrength += registers.Signal;
                }
            };

            processor.Run(instructions);

            Console.WriteLine($"Cumulative Signal Strength: {cumulativeSignalStrength}"); // 11720
        }


            processor.Run(instructions);

        }
    }
}