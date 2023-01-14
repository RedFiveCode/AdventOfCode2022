using System;
using System.Collections.Generic;
using System.IO;

namespace Day1
{
    internal class Reader
    {
        public static List<Elf> Read(string filename)
        {
            var elves = new List<Elf>();
            
            var text = File.ReadAllLines(filename);

            Elf currentElf = null;
            int elfNumber = 0;

            foreach(var line in text)
            {
                if (line.Length == 0)
                {
                    // end of current Elf, start of next Elf
                    currentElf = null;
                }
                else
                {
                    if (currentElf == null)
                    {
                        elfNumber++;
                        currentElf = new Elf(elfNumber);

                        elves.Add(currentElf);
                    }

                    currentElf.FoodItems.Add(Int32.Parse(line));
                }

            }
            return elves;
        }
    }
}
