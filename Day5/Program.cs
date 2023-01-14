using System;

namespace Day5
{
    internal partial class Program
    {
        static void Main(string[] args)
        {
            //const string filename = "Example.txt";
            const string filename = "Data.txt";

            //PartOne(filename);
            PartTwo(filename);
        }

        static void PartOne(string filename)
        {
            var moves = Reader.ReadMoves(filename);

            var stacks = Reader.ReadStacks(filename);

            var group = new GroupOfStackOfCrates(stacks);

            Console.WriteLine(group.DumpTopOfStack());

            foreach (var move in moves)
            {
                group.Rearrange(move);

                Console.WriteLine(group.DumpTopOfStack());
            }

            Console.WriteLine(group.GetTopOfStackMessage()); // "BWNCQRMDB"
        }

        static void PartTwo(string filename)
        {
            var moves = Reader.ReadMoves(filename);

            var stacks = Reader.ReadStacks(filename);

            var group = new GroupOfStackOfCrates(stacks);

            Console.WriteLine(group.DumpTopOfStack());

            foreach (var move in moves)
            {
                group.RearrangeMultipleCreatesAtOnce(move);

                Console.WriteLine(group.DumpTopOfStack());
            }

            Console.WriteLine(group.GetTopOfStackMessage()); // "MCD" / "NHWZCBNBF"
        }
    }
}
