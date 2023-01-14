namespace Day9
{
    internal class Motion
    { 
        public enum Directions { L, R, U, D }   

        public Motion(Directions direction, int steps)
        {
            Direction = direction;
            Steps = steps;
        }

        public Directions Direction { get; private set; }
        public int Steps { get; private set; }

        public override string ToString()
        {
            return $"{Direction} {Steps}";
        }

    }
}