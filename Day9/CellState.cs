namespace Day9
{
    // TODO track state of each cell in the grid
    // empty (.)
    // head (H)
    // tail (T)
    // head can be on top of tail
    // tail as visited the cell at least once

    internal class CellState
    {
        private Dictionary<CellState.CellStates, char> map;

        public CellState()
        {
            State = CellStates.Empty;

            map = new Dictionary<CellState.CellStates, char>()
            {
                { CellState.CellStates.Empty,          '.' },
                { CellState.CellStates.Head,           'H' },
                { CellState.CellStates.Tail,           'T' },
                //{ CellState.CellStates.TailHasVisited, '#' },
            };
        }

        public enum CellStates { Empty, Head, Tail }

        public void UpdateState(CellStates state)
        {
            State = state;

            if (state == CellStates.Tail)
            {
                HasTailVisited = true;
            }
        }

        public CellStates State { get; private set; }   
        public bool HasTailVisited { get; private set; }

        public override string ToString()
        {
            return $"{State} ({HasTailVisited})";
        }

        public char Text
        {
            get
            {
                if (State == CellStates.Tail) // if current point is the tail, show T and not #
                {
                    return map[State]; // T
                }

                if (HasTailVisited)
                {
                    return '#';
                }

                return map[State];
            }
        }

    }
}
