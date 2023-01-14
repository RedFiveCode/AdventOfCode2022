namespace Day9
{
    // Part One
    internal class RopeNavigatorPartOne
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

        private Point head;
        private Point tail;

        private HashSet<Point> headHistory;
        private HashSet<Point> tailHistory;

        public RopeNavigatorPartOne()
        {
            headHistory = new HashSet<Point>();
            tailHistory = new HashSet<Point>();

            tail = new Point(0, 0);
            head = new Point(0, 0);

            Tail = tail;
            Head = head;
        }

        public Point Head
        {
            get { return head; }
            set
            {
                head = value;
                headHistory.Add(value);
            }
        }

        public Point Tail
        {
            get { return tail; }
            set
            {
                tail = value;
                tailHistory.Add(value);
            }
        }

        public int TailCount
        {
            get { return tailHistory.Count; }   
        }


        public void Move(Motion motion)
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

        public string ShowGrid()
        {
            var viz = new GridVisualiser(Head, Tail, headHistory, tailHistory);
            return viz.GetText();
        }

        private void MoveStep(Motion.Directions direction)
        {
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
    }
}
