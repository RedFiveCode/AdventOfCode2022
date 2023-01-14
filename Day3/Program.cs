using System;
using System.Linq;

namespace Day3
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
            //var rucksacks = Reader.Read("example.txt");
            var rucksacks = Reader.Read("data.txt");

            int sum = 0;

            foreach (var rucksack in rucksacks)
            {
                var common = rucksack.GetCommonItems();
                if (common.Any())
                {
                    Console.WriteLine($"Common: {common.Count} : {String.Join(", ", common)}");

                    sum += common.Sum(i => i.Priority);
                }
            }

            var sum2 = rucksacks.Select(r => r.GetCommonItems().FirstOrDefault()).Sum(r => r.Priority);

            Console.WriteLine($"Summed priority: {sum}"); // 8139

        }

        static void PartTwo()
        {
            //var groups = Reader.ReadGroups("example.txt");
            var groups = Reader.ReadGroups("data.txt");

            int prioritySum = 0;
            foreach (var group in groups)
            {
                var sum = group.CommonItems.Sum(i => i.Priority);

                prioritySum += sum;

                Console.WriteLine($"Group ({group.Rucksacks.Count}) : {String.Join(", ", group.CommonItems)}, priority {sum}");
            }

            Console.WriteLine($"Priority sum: {prioritySum}"); // 2668
        }
    }
}
