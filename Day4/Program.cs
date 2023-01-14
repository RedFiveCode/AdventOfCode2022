using System;
using System.Linq;

namespace Day4
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //PartOne();
            PartTwo();
        }

        static void PartOne()
        {
            //var pairs = Reader.Read("Example.txt");
            var pairs = Reader.Read("Data.txt");

            foreach (var pair in pairs)
            {
                if (pair.IsContainedPair())
                {
                    Console.WriteLine($"{pair}: {pair.IsContainedPair()}");
                }
            }

            int containedPairCount = pairs.Where(p => p.IsContainedPair()).Count();
            Console.WriteLine($"Contained Pair Count : {containedPairCount}"); // 453
        }

        static void PartTwo()
        {
            //var pairs = Reader.Read("Example.txt");
            var pairs = Reader.Read("Data.txt");

            foreach (var pair in pairs)
            {
                if (pair.IsOverlap())
                {
                    Console.WriteLine($"{pair}: {pair.IsContainedPair()}");
                }
            }

            int overlappedPairCount = pairs.Where(p => p.IsOverlap()).Count();
            Console.WriteLine($"Overlapped Pair Count : {overlappedPairCount}"); // 919
        }
    }
}
