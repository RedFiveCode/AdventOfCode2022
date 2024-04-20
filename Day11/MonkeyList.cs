namespace Day11
{
    internal class MonkeyList
    {
        public List<Monkey> Monkeys { get; private set; }

        public MonkeyList()
        {
            Monkeys = new List<Monkey>();    
        }

        public void Add(Monkey monkey)
        {
            Monkeys.Add(monkey);
        }

        public void InspectMonkeys()
        {
            for (int m = 0; m < Monkeys.Count; m++)
            {
                var currentMonkey = Monkeys[m];

                Console.WriteLine($"Monkey {m}:");
                Console.WriteLine($"  Starting items: {String.Join(", ", currentMonkey.Items)}");
               

                foreach (var item in currentMonkey.Items)
                {
                    var destinationMonkey = currentMonkey.InspectItem(item);

                    Console.WriteLine($"  Throw item {item} to Monkey {destinationMonkey}");

                    // throw item to another monkey
                    Monkeys[destinationMonkey].Items.Add(item);
                }

                // remove all items from the current monkey; have thrown each item to another monkey
                currentMonkey.Items.Clear();
            }
        }
    }
}