using System;
using System.Diagnostics;
using System.Globalization;

namespace Day10
{
    internal class Instruction
    {
        public Instruction(string opCode, int cycles)
        {
            OpCode = opCode;
            Cycles = cycles;
        }

        public string OpCode { get; private set; }

        public int Cycles { get; private set; }


        /// <summary>
        /// Processes each tick cycle; retrns true if finished processing the instruction
        /// </summary>
        /// <param name="registers"></param>
        /// <returns></returns>
        public virtual bool Tick(Registers registers)
        {
            return registers.Tick >= Cycles;
        }
    }

    [DebuggerDisplay("{OpCode}")]
    internal class Noop : Instruction
    {
        public Noop() : base("noop", 1) { }

        public override bool Tick(Registers registers)
        {
            Console.WriteLine(OpCode);

            return true;
        }
    }

    [DebuggerDisplay("{OpCode} {Value}")]
    internal class Addx : Instruction
    {
        public Addx(int value) : base("addx", 2)
        {
            Value = value;
        }

        public int Value { get; private set; }

        public override bool Tick(Registers registers)
        {
            Console.WriteLine(OpCode);

            if (registers.Tick < Cycles)
            {
                return false;
            }

            // final tick in the cycle
            registers.X += Value;
            return true;
        }
    }
}