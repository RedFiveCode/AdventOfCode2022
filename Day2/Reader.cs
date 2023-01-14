using System.Collections.Generic;
using System.IO;

namespace Day2
{
    internal partial class Program
    {
        public class Reader
        {
            public static List<Round> Read(string filename)
            {
                var map = new Dictionary<char, Thing>()
                {
                    { 'A', Thing.Rock },
                    { 'B', Thing.Paper },
                    { 'C', Thing.Scissors },

                    { 'X', Thing.Rock },
                    { 'Y', Thing.Paper },
                    { 'Z', Thing.Scissors },
                };

                var rounds = new List<Round>();

                var text = File.ReadAllLines(filename);

                foreach (var line in text)
                {
                    var opponent = line[0];
                    var self = line[2];

                    var round = new Round(map[opponent], map[self]);

                    rounds.Add(round);
                }

                return rounds;
            }

            public static List<RoundPartTwo> ReadPartTwo(string filename)
            {
                var map = new Dictionary<char, Thing>()
                {
                    { 'A', Thing.Rock },
                    { 'B', Thing.Paper },
                    { 'C', Thing.Scissors }
                };

                var mapColumnTwo = new Dictionary<char, Outcome>()
                {
                    { 'X', Outcome.OpponentWins },
                    { 'Y', Outcome.Draw },
                    { 'Z', Outcome.SelfWins },
                };

                var rounds = new List<RoundPartTwo>();

                var text = File.ReadAllLines(filename);

                foreach (var line in text)
                {
                    var opponent = line[0];
                    var self = line[2];

                    var round = new RoundPartTwo(map[opponent], mapColumnTwo[self]);

                    rounds.Add(round);
                }

                return rounds;
            }
        }
    }
}
