using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day7
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
            var reader = new Reader();
            //var contents = reader.Read("Example.txt");
            var contents = reader.Read("Data.txt");

            // find all of the directories with a total size of at most 100000,
            // then calculate the sum of their total sizes
            int totalSize = 0;
            int maximumSize = 100000;

            var allSubFolders = contents.GetSubFolders();
            foreach (var sub in allSubFolders)
            {
                sub.CalculateFolderSize();

                if (sub.Size <= maximumSize)
                {
                    totalSize += sub.Size;
                }
            }

            Console.WriteLine(totalSize); // 1783610
        }

        static void PartTwo()
        {
            // Find the smallest directory that,
            // if deleted, would free up enough space on the filesystem to run the update.
            // What is the total size of that directory?
            const int TotalSpace = 70000000;
            const int MinimumFreeSpace = 30000000;



            var reader = new Reader();
            //var contents = reader.Read("Example.txt");
            var contents = reader.Read("Data.txt");
            contents.CalculateFolderSize(); // root

            var allSubFolders = contents.GetSubFolders();

            foreach (var sub in allSubFolders)
            {
                sub.CalculateFolderSize();
            }

            var usedSpace = contents.Size;
            var freeSpace = TotalSpace - usedSpace;
            var availableSpace = MinimumFreeSpace - freeSpace; // minimum amount to delete

            var result = allSubFolders.Where(f => f.Size >= availableSpace)
                                      .OrderBy(f => f.Size)
                                      .FirstOrDefault();
                

            
            Console.WriteLine($"{result.Name} {result.Size}"); // tdnp, 4370655
        }

        
   }
}
