using System;
using System.Linq;

namespace Day2
{
    internal partial class Program
    {
        static void Main(string[] args)
        {
            //PartOne();
            PartTwo();
        }

        static void PartOne()
        {
            //var rounds = Reader.Read("example.txt");
            var rounds = Reader.Read("data.txt");

            foreach (var round in rounds)
            {
                Console.WriteLine(round);
            }

            var totalScore = rounds.Sum(r => r.SelfScore);

            Console.WriteLine($"My total score {totalScore}"); // 14264
        }

        static void PartTwo()
        {
            //var rounds = Reader.ReadPartTwo("example.txt");
            var rounds = Reader.ReadPartTwo("data.txt");

            foreach (var round in rounds)
            {
                Console.WriteLine(round);
            }

            var totalScore = rounds.Sum(r => r.SelfScore);

            Console.WriteLine($"My total score {totalScore}"); // 12382
        }
    }
}
