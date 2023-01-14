using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace Day5
{
    internal partial class Program
    {
        internal class Reader
        {
            public static Dictionary<int, StackOfCrates> ReadStacks(string filename)
            {
                var crates = new Dictionary<int, StackOfCrates>();

                // 0123456789...
                //     [D]    
                // [N] [C]
                // [Z] [M] [P]
                //  1   2   3

                var regexCreate = new Regex(@"(\[.\])");
                var regexStack = new Regex(@"\s+(\d+)\s+");

                var lines = File.ReadLines(filename);

                var crateLines = lines.Where(line => !line.StartsWith("move") && line.Length > 0).ToList();

                crateLines.Reverse();

                //var crateLines = lines.Reverse().ToList();
                // 0123456789...
                //  1   2   3
                // [Z] [M] [P]
                // [N] [C]
                //     [D]    

                // get offsets of each crate letter
                // will use the offset to index into each line
                var idLine = crateLines.First().TrimEnd(' ');
                var offsetMap = new Dictionary<int, int>();
                for (int i = 0; i < idLine.Length; i++)
                {
                    var current = idLine[i];

                    if (current >= '1' && current <= '9')
                    {
                        offsetMap.Add(i, Int32.Parse(new string(current, 1)));
                    }
                }

                // create stacks of without crates
                var numberofStacks = offsetMap.Count;
                for (int id = 1; id <= numberofStacks; id++)
                {
                    crates.Add(id, new StackOfCrates(id));
                }

                // read stack lines "[Z] [M] [P]"...
                foreach (var crateLine in crateLines.Skip(1))
                {
                    //for (int currentStackId = 1; currentStackId <= numberofStacks; currentStackId++)
                    foreach(var item in offsetMap)
                    {
                        var ch = crateLine[item.Key]; //  offset for current stack

                        if (ch >= 'A' && ch <= 'Z')
                        {
                            // found a crate in the current stack
                            crates[item.Value].Stacks.Push(new Crate(ch));
                        }
                    }

                }

                return crates;
            }

            public static List<Move> ReadMoves(string filename)
            {
                var moves = new List<Move>();

                var lines = File.ReadLines(filename);

                foreach(var line in lines)
                {
                    var move = ReadMove(line);

                    if (move != null)
                    {
                        moves.Add(move);
                    }
                }

                return moves;
            }

            private static Move ReadMove(string line)
            {
                // "move 1 from 2 to 1"

                var regex = new Regex(@"move (\d+) from (\d+) to (\d+)");
                var match = regex.Match(line);

                if (match.Success)
                {
                    int id = Int32.Parse(match.Groups[1].Value);
                    int from = Int32.Parse(match.Groups[2].Value);
                    int to = Int32.Parse(match.Groups[3].Value);

                    return new Move(id, from, to);
                }
                else
                {
                    return null;
                }
                    
            }
        }
    }
}
