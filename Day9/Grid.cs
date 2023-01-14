using Day9;
using System;
using System.Diagnostics;
using System.Text;

namespace Day9
{

    [DebuggerDisplay("Head={head}, Tail={tail}")]
    internal class Grid
    {
        private const int rows = 5;
        private const int columns = 6;

        private Dictionary<Point, CellState> points;

        private Point head;
        private Point tail;

        public Grid() 
        {
            points = new Dictionary<Point, CellState>();

            for (int row = 0; row < rows; row++)
            {
                for (int col = 0; col < columns; col++)
                {
                    var point = new Point(col, row);
                    points.Add(point, new CellState());
                }
            }

            tail = new Point(0, 0);
            head = new Point(0, 0);

            Tail = tail;
            Head = head;
        }

        public int GetTailVisitedCount()
        {
            return points.Where(kvp => kvp.Value.HasTailVisited).Count();
        }

        public void Move(Motion motion)
        {
            Console.WriteLine($"== {motion} ==");

            // move in discrete steps
            for (int steps = 1; steps <= motion.Steps; steps++)
            {
                MoveStep(motion.Direction);

                Console.WriteLine(GetText());
            }
        }

        public Point Head
        {
            get { return head; }
            set
            {
                // sanity check
                VerifyPoint(value, rows, columns);

                // reset old value
                points[head].UpdateState(CellState.CellStates.Empty);

                head = value;
                points[head].UpdateState(CellState.CellStates.Head);
            }
        }

        public Point Tail
        {
            get { return tail; }
            set
            {
                // sanity check
                VerifyPoint(value, rows, columns);

                // reset old value
                points[tail].UpdateState(CellState.CellStates.Empty);

                tail = value;
                points[tail].UpdateState(CellState.CellStates.Tail);
            }
        }

        public string GetText()
        {
            var builder = new StringBuilder();   

            for (int row = rows - 1; row >= 0; row--)
            {
                for (int col = 0; col < columns; col++)
                {
                    var currentPoint = new Point(col, row);
                    var p = points[currentPoint];

                    builder.Append(p.Text);
                }

                builder.AppendLine();
            }

            return builder.ToString();
        }

        private void MoveStep(Motion.Directions direction)
        {
            const int MinimumDistance = 2;

            // move head
            var m = new Motion(direction, 1);

            var newHead = MoveTo(Head, m);
            Head = newHead;

            // move head, tail follows:
            // If the head is ever two steps directly up, down, left, or right from the tail,
            // the tail must also move one step in that direction so it remains close enough.
            var distance = Distance(Head, Tail);
            if (distance == MinimumDistance && (IsSameRow(Head, Tail) || IsSameColumn(Head, Tail)))
            {
                Console.WriteLine($"Moving Tail 1 step {direction}");

                var newTail = MoveTo(Tail, new Motion(direction, 1));
                Tail = newTail;

                return;
            }

            // Otherwise, if the head and tail aren't touching and aren't in the same row or column,
            // the tail always moves one step diagonally to keep up.
            if (!IsTouching(head, Tail) && !(IsSameRow(Head, Tail) || IsSameColumn(Head, Tail)))
            {
                // work out diagonal direction
                // can implement as two calls to MoveTo, but need to know NW/NE/SE/SW
                // H.x > T.x -> East (R), H.x < T.x -> West (L)
                // H.y > T.y -> North (U), H.y < T.y -> South (D)

                var newTail = new Point(Tail.X, Tail.Y);

                var first = (Head.X > Tail.X ? Motion.Directions.R : Motion.Directions.L);
                var second = (Head.Y > Tail.Y ? Motion.Directions.U : Motion.Directions.D);

                Console.WriteLine($"Moving Tail diagonally 1 step {first} then {second}");

                newTail = MoveTo(newTail, new Motion(first, 1));
                newTail = MoveTo(newTail, new Motion(second, 1));

                Tail = newTail;
            }
        }

        private static int Distance(Point p1, Point p2)
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

        private static Point MoveTo(Point from, Motion motion)
        {
            var to = new Point(from.X, from.Y);

            switch (motion.Direction)
            {
                // TODO wrap around???

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

        private static void VerifyPoint(Point p, int rows, int cols)
        {
            if (p == null)
            {
                throw new ArgumentNullException(nameof(p));
            }

            if (p.X < 0 || p.X >= cols)
            {
                throw new ArgumentOutOfRangeException($"Point X value out of range");
            }

            if (p.Y < 0 || p.Y >= cols)
            {
                throw new ArgumentOutOfRangeException($"Point Y value out of range");
            }
        }
    }
}
