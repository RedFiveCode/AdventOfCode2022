namespace Day9
{
    internal class Rope
    {
        public Rope()
        {
            Knots = new List<Knot>();
        }

        public Knot AddHeadKnot(string id, int x, int y)
        {
            var knot = new Knot(id, Knot.KnotType.Head, x, y);
            Knots.Add(knot);

            return knot;
        }

        public Knot AddTailKnot(string id, int x, int y)
        {
            var knot = new Knot(id, Knot.KnotType.Tail, x, y);
            Knots.Add(knot);

            return knot;
        }

        public List<Knot> Knots { get; private set; }

    }
}
