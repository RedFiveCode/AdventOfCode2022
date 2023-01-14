using System.Diagnostics;

namespace Day9
{
    [DebuggerDisplay("Knot Id={Id}, Type={Type}, X={Point.X}, Y={Point.Y}")]
    internal class Knot
    {
        public enum KnotType { Head = 0, Tail = 1 }

        public Knot(string id, KnotType knotType)
        {
            Id = id;
            Point = new Point(0, 0);
            Type = knotType;
        }

        public Knot(string id, KnotType knotType, int x, int y)
        {
            Id = id;
            Point = new Point(x, y);
            Type = knotType;
        }

        public string Id { get; private set; }
        public Point Point { get; private set; }
        public KnotType Type { get; private set; }

        public void Move(Point to)
        {
            Point.X = to.X;
            Point.Y = to.Y;
        }
    }
}
