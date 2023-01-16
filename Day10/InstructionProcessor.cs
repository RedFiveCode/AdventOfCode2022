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

            while (Registers.PC < instructions.Count)
            {
                var currentInstruction = instructions[registers.PC];
                registers.Current = currentInstruction;

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

                    if (CycleCompleted != null)
                    {
                        CycleCompleted(this, registers);
                    }
                }

                if (InstructionCompleted != null)
                {
                    InstructionCompleted(this, registers);
                }

                registers.PC++;
            }

            Console.WriteLine("Finished");
            Console.WriteLine(registers.ToString());
        }

        public event EventHandler<Registers> InstructionCompleted; // not used; YAGNI?
        public event EventHandler<Registers> CycleCompleted;

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