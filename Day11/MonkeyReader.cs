using System.Text.RegularExpressions;

namespace Day11
{
    internal class MonkeyReader
    {
        public static List<Monkey> ReadFile(string filename)
        {
            var text = File.ReadAllLines(filename);

            return Read(text);
        }

        public static List<Monkey> Read(string[] data)
        {
            var monkeys = new List<Monkey>();

            var regexId = new Regex(@"Monkey \d+");

            //Monkey 0:
            //  Starting items: 79, 98
            //  Operation: new = old * 19
            //  Test: divisible by 23
            //    If true: throw to monkey 2
            //    If false: throw to monkey 3
            //(blank line)

            foreach (var line in data.Chunk(7))
            {
                var match = regexId.Match(line[0]);
                if (!match.Success)
                {
                    throw new ArgumentException($"No match for monkey id on line '{line[0]}'");
                }

                var id = Int32.Parse(match.Groups[1].Value);


            }

            return monkeys;
        }
    }
}