using System.Diagnostics;

namespace Day9
{
    [DebuggerDisplay("X={X}, Y={Y}")]
    internal class Point : IEquatable<Point>
    {
        public Point (int x, int y)
        {
            X = x;
            Y = y;
        }

        public int X { get; set; }
        public int Y { get; set; }

        public override string ToString()
        {
            return $"({X},{Y})";
        }

        public override int GetHashCode()
        {
            // https://stackoverflow.com/questions/371328/why-is-it-important-to-override-gethashcode-when-equals-method-is-overridden
            //unchecked // Overflow is fine, just wrap
            //{
            //    int hash = 17;

            //    hash = (hash * 23) + X.GetHashCode();
            //    hash = (hash * 23) + Y.GetHashCode();

            //    return hash;
            //}

            return (X,Y).GetHashCode(); // tuple
            //return HashCode.Combine(X, X);
        }

        public bool Equals(Point? other)
        {
            if (other == null)
            {
                return false;
            }

            return other.X == X && other.Y == Y;
        }
    }
}
