using System;
using System.Collections.Generic;

namespace Day10
{
    internal class InstructionProcessor
    {
        private Registers registers;
        private List<int> cyclesOfInterest;
        public InstructionProcessor()
        {
            registers = new Registers();

            cyclesOfInterest = new List<int>()
            {
                20, 60, 100, 140, 180, 220
            };
        }

        public void Run(List<Instruction> instructions)
        {
            registers.PC = 0;
            registers.Cycles = 1;
            registers.Tick = 0;
            registers.X = 1;

            int cumulativeSignalStrength = 0;

            while (Registers.PC < instructions.Count)
            {
                var currentInstruction = instructions[registers.PC];
                registers.Tick = 1;

                // tick cycle for curent instruction
                bool finishedCurrentInstruction = false;
                while (!finishedCurrentInstruction)
                {
                    finishedCurrentInstruction = currentInstruction.Tick(registers);
                    
                    registers.Cycles++;

                    if (!finishedCurrentInstruction)
                    {
                        registers.Tick++;
                    }

                    if (IsCycleOfInterest())
                    {
                        DumpState(currentInstruction);

                        cumulativeSignalStrength += registers.Signal;
                    }
                }

                registers.PC++;
            }

            Console.WriteLine("Finished");
            Console.WriteLine(registers.ToString());
            Console.WriteLine($"Cumulative Signal Strength: {cumulativeSignalStrength}"); // 11720
        }

        private bool IsCycleOfInterest()
        {
            return cyclesOfInterest.Contains(registers.Cycles);
        }

        private void DumpState(Instruction currentInstruction)
        {
            Console.WriteLine(registers.ToString());
        }
 
        public Registers Registers { get { return registers; } }
    }
}