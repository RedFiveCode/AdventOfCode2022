using System.Diagnostics;
using System.Security.AccessControl;

namespace Day9
{
    // Part Two
    internal class RopeNavigator
    {
        // We move the head and tail around as points (x,y) coordinates on a grid.
        // However, we don't have a grid class (2-D array or equivalent) as we
        // don't know the size of such a grid,
        // and we only actually need such a grid to visualise the
        // current head and tail positions to assist in debugging.
        //
        // We can discover a grid at run time based on the largest X and Y coordinates
        // of all the points that have been visited, 
        // and in turn use this to visualise the rope.

        const int MinimumDistance = 2;

        private HashSet<Point> tailHistory;

        private Rope rope;

        public RopeNavigator(int numberOfKnots)
        {
            rope = new Rope();
            tailHistory = new HashSet<Point>();

            // numberOfKnots 10 means head and 9 knots
            for (int i = 0; i < numberOfKnots; i++)
            {
                if (i == 0)
                {
                    var knot = rope.AddHeadKnot($"Head", 0, 0);
                }
                else
                {
                    var knot = rope.AddTailKnot($"Tail #{i}", 0, 0);
                }
            }
        }

        public int TailCount
        {
            get { return tailHistory.Count; }
        }

        public bool ContainsTail(int x, int y)
        {
            return tailHistory.Contains(new Point(x, y));
        }

        public void Move(List<Motion> motions)
        {
            foreach (var motion in motions)
            {
                Console.WriteLine($"== {motion} ==");

                // move in discrete steps
                for (int steps = 1; steps <= motion.Steps; steps++)
                {
                    MoveStep(motion.Direction);

                    // visualise after each step
                    //Console.WriteLine(ShowGrid());
                }

                // visualise after each motion
                //Console.WriteLine(ShowGrid());

            }
        }

        public string ShowGrid()
        {
            var viz = new GridVisualiser(rope.Knots, tailHistory);
            return viz.GetText();
        }

        private void MoveStep(Motion.Directions direction)
        {
            // move head one step  in specified direction

            var newHead = MoveOneStep(rope.Knots.First().Point, direction);
            Console.WriteLine($"Move Knot '{rope.Knots.First().Id}' 1 step {direction} from {rope.Knots.First().Point} to {newHead}");
            rope.Knots.First().Move(newHead);

            // move tail(s) to follow head
            for (int i = 1; i < rope.Knots.Count; i++) // start at first tail knot (not the head)
            {
                var currentKnot = rope.Knots[i];
                var neighbouringKnot = rope.Knots[i - 1];

                // TODO move knot[i] with respect to neighbouring knot (knot[i-1])
                // for knot[1], the neighbour is the head (knot[0]) 
                // for knot[2] onwards, the neighbour is the head (knot[i-1], and not with respect to the head

                // move head, tail follows:
                // If the head is ever two steps directly up, down, left, or right from the tail,
                // the tail must also move one step in that direction so it remains close enough.
                // Otherwise, if the head and tail aren't touching and aren't in the same row or column,
                // the tail always moves one step diagonally to keep up.

                var distance = Distance(currentKnot.Point, neighbouringKnot.Point);

                if (distance >= MinimumDistance)
                {
                    // move horizontally or vertically                   
                    if (IsSameRow(currentKnot.Point, neighbouringKnot.Point) || IsSameColumn(currentKnot.Point, neighbouringKnot.Point))
                    {
                        // move horizontally or vertically
                        // the direction of the tail knot is not always the same as the direction of the head

                        Motion.Directions d;
                        if (IsSameRow(currentKnot.Point, neighbouringKnot.Point))
                        {
                            // move horizontally
                            d = (neighbouringKnot.Point.X > currentKnot.Point.X ? Motion.Directions.R : Motion.Directions.L);
                        }
                        else
                        {
                            // move vertically
                            d = (neighbouringKnot.Point.Y > currentKnot.Point.Y ? Motion.Directions.U : Motion.Directions.D);
                        }

                        var newTail = MoveOneStep(currentKnot.Point, d);
                        Console.WriteLine($"Move Knot '{currentKnot.Id}' {direction} from {currentKnot.Point} to {newTail}");
                        currentKnot.Move(newTail);

                        AppendTailHistory(rope.Knots.Last().Point);
                    }
                    else
                    {
                        // move diagagonally

                        // work out diagonal direction
                        // can implement as two calls to MoveTo, but need to know NW/NE/SE/SW
                        // H.x > T.x -> East (R), H.x < T.x -> West (L)
                        // H.y > T.y -> North (U), H.y < T.y -> South (D)

                        var first = (neighbouringKnot.Point.X > currentKnot.Point.X ? Motion.Directions.R : Motion.Directions.L);
                        var second = (neighbouringKnot.Point.Y > currentKnot.Point.Y ? Motion.Directions.U : Motion.Directions.D);

                        var newTail = new Point(currentKnot.Point.X, currentKnot.Point.Y);
                        newTail = MoveOneStep(newTail, first);
                        newTail = MoveOneStep(newTail, second);

                        Console.WriteLine($"Move Knot '{currentKnot.Id}' diagonally {first} then {second} from {currentKnot.Point} to {newTail}");
                        currentKnot.Move(newTail);

                        AppendTailHistory(rope.Knots.Last().Point);
                    }
                }
            }
        }

        private void AppendTailHistory(Point p)
        {
            var clone = new Point(p.X, p.Y); // make a copy of the point when adding to the hashset
            tailHistory.Add(clone);
        }


        private static int Distance(Point p1, Point p2) // grid horizontal & vertical distance (manhattan)
        {
            var dx = Math.Abs(p1.X - p2.X);
            var dy = Math.Abs(p1.Y - p2.Y);

            return Math.Max(dx, dy);
        }

        private static bool IsSameRow(Point p1, Point p2)
        {
            return p1.Y == p2.Y;
        }
        
        private static bool IsSameColumn(Point p1, Point p2)
        {
            return p1.X == p2.X;
        }

        private static bool IsTouching(Point p1, Point p2)
        {
            return Distance(p1, p2) <= 1;
        }

        private static Point MoveOneStep(Point from, Motion.Directions direction)
        {
            var to = new Point(from.X, from.Y);

            switch (direction)
            {
                case Motion.Directions.R: // right
                    {
                        to.X++;
                    }
                    break;

                case Motion.Directions.L: // left
                    {
                        to.X--;
                    }
                    break;

                case Motion.Directions.U: // up
                    {
                        to.Y++;
                    }
                    break;

                case Motion.Directions.D: // down
                    {
                        to.Y--;
                    }
                    break;
            }

            return to;
        }
        private static Point MoveTo(Point from, Motion motion)
        {
            var to = new Point(from.X, from.Y);

            switch (motion.Direction)
            {
                case Motion.Directions.R: // right
                    {
                        to.X += motion.Steps;
                    }
                    break;

                case Motion.Directions.L: // left
                    {
                        to.X -= motion.Steps;
                    }
                    break;

                case Motion.Directions.U: // up
                    {
                        to.Y += motion.Steps;
                    }
                    break;

                case Motion.Directions.D: // down
                    {
                        to.Y -= motion.Steps;
                    }
                    break;
            }

            return to;
        }
    }
}
