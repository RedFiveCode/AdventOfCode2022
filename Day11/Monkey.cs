namespace Day11
{
    internal class Monkey
    {
        private Func<int, int> operation;
        int divisibleBy;
        int destinationTrue;
        int destinationFalse;

        public Monkey()
        {
            Items = new List<int>();
        } 

        public Monkey(int id, List<int> startingItems, Func<int, int> operation, int divisibleBy, int testTrueMonkeyId, int testFalseMonkeyId)
        {
            Id = id;
            Items = new List<int>();
            Items.AddRange(startingItems);
            this.operation = operation;
            this.divisibleBy = divisibleBy;
            destinationTrue = testTrueMonkeyId;
            destinationFalse = testFalseMonkeyId;
        }
        public int Id { get; private set; }

        public List<int> Items { get; private set; }


        /// <summary>
        /// Inspect the monkey with <see cref="worryLevel"/>
        /// and return the monkey Id to throw thr item to.
        /// </summary>
        /// <param name="worryLevel"></param>
        /// <returns></returns>
        public int InspectItem(int worryLevel)
        {
            int newWorryLevel = operation(worryLevel);

            newWorryLevel = PostInspect(newWorryLevel);

            bool testResult = IsDivisibleBy(newWorryLevel, divisibleBy);

            int destinationMonkey = testResult ? destinationTrue : destinationFalse;

            return destinationMonkey;
        }


        private int PostInspect(int value)
        {
            // divided by three and rounded down to the nearest integer
            int result = value / 3;
            return (int)Math.Round((double)result);
        }


        private static bool IsDivisibleBy(int value, int dividor)
        {
            return value % dividor == 0;
        }
    }
}