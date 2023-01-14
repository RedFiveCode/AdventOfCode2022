using System.Collections.Generic;
using System.Linq;

namespace Day1
{
    class Elf
    {
        public Elf(int id)
        {
            Id = id;
            FoodItems = new List<int>();
        }
        public int Id { get; private set; }
        public List<int> FoodItems { get; private set; }

        public int Calories
        {
            get { return FoodItems.Sum();  }
        }
    }
}
