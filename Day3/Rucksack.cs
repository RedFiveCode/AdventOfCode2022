using System;
using System.Collections.Generic;
using System.Linq;

namespace Day3
{
    internal class Rucksack
    {
        public Rucksack()
        {
            Items = new List<Item>();

            FirstCompartment = null;
            SecondCompartment = null; 
        }

        public List<Item> Items { get; private set; }

        public List<Item> FirstCompartment { get; private set; }

        public List<Item> SecondCompartment { get; private set; }

        public void ProcessItems()
        {
            int boundary = Items.Count / 2;

            FirstCompartment = Items.Take(boundary).ToList();    

            SecondCompartment = Items.Skip(boundary).Take(boundary).ToList();
        }

        public List<Item> GetCommonItems()
        {
            return FirstCompartment.Intersect(SecondCompartment).ToList();
        }

        public override string ToString()
        {
            return String.Join(", ", Items);    
        }
    }

    internal class Group
    {
        public Group(IList<Rucksack> rucksacks)
        {
            Rucksacks = new List<Rucksack>(rucksacks);

            CommonItems = GetCommonItems(rucksacks);
        }

        public List<Rucksack> Rucksacks { get; private set; }

        public List<Item> CommonItems { get; private set; }

        private List<Item> GetCommonItems(IList<Rucksack> rucksacks)
        {
            if (rucksacks.Count() != 3)
            {
                throw new InvalidOperationException();
            }

            return rucksacks[0].Items.Intersect(rucksacks[1].Items)
                                     .Intersect(rucksacks[2].Items)
                                      .ToList();
        }
    }
}
