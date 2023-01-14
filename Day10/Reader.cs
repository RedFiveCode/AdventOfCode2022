using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace Day10
{
    internal class Reader
    {
        public static List<Instruction> Read(string filename)
        {
            var lines = File.ReadLines(filename);

            return lines.Select(line => Parse(line)).ToList();
        }

        private static Instruction Parse(string line)
        {
            if (line == "noop")
            {
                return new Noop();
            }
            else if (line.StartsWith("addx"))
            {
                var regex = new Regex(@"addx (-?\d+)");

                var match = regex.Match(line);
                if (match != null && match.Success)
                {
                    var arg = Int32.Parse(match.Groups[1].Value);

                    return new Addx(arg);
                }
                else
                {
                    throw new ArgumentOutOfRangeException($"addx instruction regex mismatch for '{line}'");
                }
            }
            else
            {
                throw new ArgumentOutOfRangeException($"Unexpected instruction for '{line}'");
            }
        }
    }
}