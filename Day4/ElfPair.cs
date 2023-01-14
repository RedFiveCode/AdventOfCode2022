using System.Linq;

namespace Day4
{
    internal class ElfPair
    {
        public ElfPair(Elf first, Elf second)
        {
            First = first;
            Second = second;
        }

        public Elf First { get; private set; }
        public Elf Second { get; private set; }

        public bool IsContainedPair()
        {
            return First.Sections.Intersect(Second.Sections).Count() == Second.Count
                || Second.Sections.Intersect(First.Sections).Count() == First.Count;

        }

        public bool IsOverlap() // part two
        {
            return First.Sections.Intersect(Second.Sections).Any() || Second.Sections.Intersect(First.Sections).Any();
        }

        public override string ToString()
        {
            return $"{First}, {Second}";
        }
    }
}
