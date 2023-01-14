using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;

namespace Day4
{
    internal class Reader
    {
        public static List<ElfPair> Read(string filename)
        {
            var elfPairs = new List<ElfPair>();

            var text = File.ReadAllLines(filename);

            foreach (var line in text)
            {
                elfPairs.Add(Parse(line));
            }

            return elfPairs;
        }

        private static ElfPair Parse(string line)
        {
            // https://regex101.com/r/Ehh31U/1/
            var regex = new Regex("(?<firstStart>\\d+)-(?<firstFinish>\\d+),(?<secondStart>\\d+)-(?<secondFinish>\\d+)");

            var match = regex.Match(line);

            if (match != null && match.Success)
            {

                var firstStart = Int32.Parse(match.Groups["firstStart"].Value);
                var firstFinish = Int32.Parse(match.Groups["firstFinish"].Value);
                var first = new Elf(firstStart, firstFinish);

                var secondStart = Int32.Parse(match.Groups["secondStart"].Value);
                var secondFinish = Int32.Parse(match.Groups["secondFinish"].Value);
                var second = new Elf(secondStart, secondFinish);

                return new ElfPair(first, second);
            }

            throw new InvalidOperationException("Regex error");
        }

        private static int ParseChar(char ch)
        {
            return Int32.Parse(ch.ToString());
        }
    }
}
