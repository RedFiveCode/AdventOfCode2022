namespace Day10
{
    internal class Registers
    {
        /// <summary>
        /// X register
        /// </summary>
        public int X { get; set; }

        /// <summary>
        /// Tick cycle counter for current instruction
        /// </summary>
        public int Tick { get; set; }

        /// <summary>
        /// Program counter (offset)
        /// </summary>
        public int PC { get; set; }

        /// <summary>
        /// Cycle counter
        /// </summary>
        public int Cycles { get; set; }

        /// <summary>
        /// Signal strength
        /// </summary>
        public int Signal { get { return X * Cycles; } }

        public override string ToString()
        {
            return $"PC={PC}; T={Tick}; Cycles={Cycles}; X={X}; Signal={Signal}";
        }
    }
}