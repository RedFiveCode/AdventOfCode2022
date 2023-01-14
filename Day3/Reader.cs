using System.Collections.Generic;
using System.IO;

namespace Day3
{
    class Reader
    {
        public static List<Rucksack> Read(string filename)
        {
            var rucksacks = new List<Rucksack>();

            var text = File.ReadAllLines(filename);

            foreach(var line in text)
            {
                var rucksack = Parse(line);
                rucksacks.Add(rucksack);
            }

            return rucksacks;
        }

        public static List<Group> ReadGroups(string filename)
        {
            var groups = new List<Group>();

            var text = File.ReadAllLines(filename);

            for (int i = 0; i < text.Length; i += 3)
            {

                var line1 = text[i];
                var line2 = text[i + 1];
                var line3 = text[i + 2];

                var rucksack1 = Parse(line1);
                var rucksack2 = Parse(line2);
                var rucksack3 = Parse(line3);

                var group = new Group(new List<Rucksack>() { rucksack1, rucksack2, rucksack3 });

                groups.Add(group);
            }

            return groups;
        }

        private static Rucksack Parse(string line)
        {
            var rucksack = new Rucksack();

            for (int i = 0; i < line.Length; i++)
            {
                rucksack.Items.Add(new Item(line[i]));
            }

            rucksack.ProcessItems();

            return rucksack;
        }
    }
}
