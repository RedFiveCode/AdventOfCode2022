using System;
using System.Linq;

namespace Day1
{
    internal class Program
    {
        static void Main(string[] args)
        {
            PartOne();
            PartTwo();
        }

        static void PartOne()
        {
            //var elves = Reader.Read("ExampleData.txt");
            var elves = Reader.Read("Data.txt");

            Console.WriteLine($"Elves: {elves.Count}");

            var mostCalories = elves.Max(e => e.Calories);
            var elfWithMostCalories = elves.Where(e => e.Calories == mostCalories).First();

            Console.WriteLine($"Elves: {elves.Count}");
            Console.WriteLine($"Elf with most calories: #{elfWithMostCalories.Id}, calories {elfWithMostCalories.Calories}"); // 71506
        }

        static void PartTwo()
        {
            //var elves = Reader.Read("ExampleData.txt");
            var elves = Reader.Read("Data.txt");

            var topThreeCalories = elves.OrderByDescending(e => e.Calories).Take(3).Sum(e => e.Calories);

            Console.WriteLine($"Top 3 elves with most calories: {topThreeCalories}"); // 209603
        }
    }
}
