using System.Collections.Generic;
using System.Linq;

namespace Day4
{
    internal class Elf
    {
        public Elf(int start, int finish)
        {
            Start = start;
            Finish = finish;
            Count = finish - start + 1; // inclusive range

            Sections = Enumerable.Range(Start, Count).ToList();
        }
        public List<int> Sections { get; private set; }

        public int Start { get; private set; }
        public int Finish { get; private set; }
        public int Count { get; private set; }

        public override string ToString()
        {
            return $"{Start}-{Finish} ({Count})";
        }
    }
}
