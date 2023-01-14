using System;
using System.Collections.Generic;
using System.Text;

namespace Day5
{
    internal partial class Program
    {
        internal class Move
        {
            public Move (int count, int from, int to)
            {
                Count = count;
                From = from;
                To = to;
            }

            /// <summary>
            /// Get the number of crates to move
            /// </summary>
            public int Count { get; private set; }
            
            /// <summary>
            /// Starting stack
            /// </summary>
            public int From { get; private set; }
            
            /// <summary>
            /// Destination stack
            /// </summary>
            public int To { get; private set; }

            public override string ToString()
            {
                return $"{Count}: from {From} to {To}";
            }
        }

        internal class Crate
        {
            public Crate(char name)
            {
                Name = new string(name, 1);
            }

            public Crate(string name)
            {
                Name = name;    
            }

            public string Name { get; private set; }

            public override string ToString()
            {
                return Name;
            }
        }

        internal class StackOfCrates
        {
            public StackOfCrates(int id)
            {
                Id = id;
                Stacks = new Stack<Crate>(); // empty stack of crates
            }


            public int Id { get; private set; }
            public Stack<Crate> Stacks { get; private set; }

            public override string ToString()
            {
                return $"{Id}: Count {Stacks.Count}";
            }
        }

        internal class GroupOfStackOfCrates
        {
            private Dictionary<int, StackOfCrates> map;

            public GroupOfStackOfCrates(Dictionary<int, StackOfCrates> map)
            {
                this.map = map;
            }

            public void Rearrange(Move move)
            {
                Console.WriteLine($"Moving {move.Count} Crate(s) from Stack {move.From} to Stack {move.To}..."); 

                for (int i = 1; i <= move.Count; i++)
                {
                    var from = map[move.From];
                    var to = map[move.To];

                    var crateToMove = from.Stacks.Pop();

                    Console.WriteLine($"  Moving Crate {crateToMove.Name} from Stack {from.Id} to Stack {to.Id}");
                    
                    to.Stacks.Push(crateToMove);
                }
            }

            public void RearrangeMultipleCreatesAtOnce(Move move) // Part two
            {
                // The CrateMover 9001 is notable for many new and exciting features:
                // air conditioning, leather seats, an extra cup holder,
                // and the ability to pick up and move multiple crates at once.

                Console.WriteLine($"Moving {move.Count} Crate(s) from Stack {move.From} to Stack {move.To}...");

                var from = map[move.From];
                var to = map[move.To];

                var cratesToMove = new List<Crate>();

                for (int i = 1; i <= move.Count; i++)
                {
                    cratesToMove.Add(from.Stacks.Pop());
                }

                cratesToMove.Reverse();

                foreach (var crateToMove in cratesToMove)
                { 
                    Console.WriteLine($"  Moving Crate {crateToMove.Name} from Stack {from.Id} to Stack {to.Id}");

                    to.Stacks.Push(crateToMove);
                }
            }

            public string DumpTopOfStack(bool verbose = true)
            {
                var builder = new StringBuilder();

                for (int stackId = 1; stackId <= map.Count; stackId++)
                {
                    string value;
                    if (map[stackId].Stacks.Count == 0) // empty stack
                    {
                        value = " ";
                    }
                    else
                    {
                        value = map[stackId].Stacks.Peek().Name;
                    }

                    if (verbose)
                    {
                        builder.Append($"[{value}] ");
                    }
                    else
                    {
                        builder.Append(value);
                    }
                }

                return builder.ToString();
            }

            public string GetTopOfStackMessage()
            {
                return DumpTopOfStack(false);
            }
        }
    }
}
