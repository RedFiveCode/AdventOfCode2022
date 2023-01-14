using System.ComponentModel.DataAnnotations;
using System.Net.NetworkInformation;
using System.Text;

namespace Day9
{
    internal class GridVisualiser
    {
        private int minY; // minimum and maximum row (Y) coordinates
        private int maxY;

        private int minX; // minimum and maximum column (X) coordinates
        private int maxX;

        private Dictionary<Point, CellState> grid;

        private Dictionary<CellState, char> map = new Dictionary<CellState, char>()
        {
                { CellState.Origin,         's' },
                { CellState.Empty,          '.' },
                { CellState.Head,           'H' },
                { CellState.Tail,           'T' },
                { CellState.Tail1,          '1' },
                { CellState.Tail2,          '2' },
                { CellState.Tail3,          '3' },
                { CellState.Tail4,          '4' },
                { CellState.Tail5,          '5' },
                { CellState.Tail6,          '6' },
                { CellState.Tail7,          '7' },
                { CellState.Tail8,          '8' },
                { CellState.Tail9,          '9' },
                { CellState.TailHasVisited, '#' },
        };

        private Dictionary<string, CellState> mapId = new Dictionary<string, CellState>()
        {
                { "Tail #1", CellState.Tail1 },
                { "Tail #2", CellState.Tail2 },
                { "Tail #3", CellState.Tail3 },
                { "Tail #4", CellState.Tail4 },
                { "Tail #5", CellState.Tail5 },
                { "Tail #6", CellState.Tail6 },
                { "Tail #7", CellState.Tail7 },
                { "Tail #8", CellState.Tail8 },
                { "Tail #9", CellState.Tail9 },
        };

        private enum CellState { Origin, Empty, Head,
                                 Tail,
                                 Tail1, Tail2, Tail3, Tail4, Tail5, Tail6, Tail7, Tail8, Tail9,
                                 TailHasVisited }

        public GridVisualiser(Point head, Point tail, HashSet<Point> headHistory, HashSet<Point> tailHistory)
        {           
            // discover the largest X and Y coordinates of all the points that have been visited
            //minY = Math.Min(tailHistory.Min(p => p.Y), tailHistory.Min(p => p.Y));
            //maxY = Math.Max(tailHistory.Max(p => p.Y), tailHistory.Max(p => p.Y));

            //minX = Math.Min(tailHistory.Min(p => p.X), tailHistory.Min(p => p.X));
            //maxX = Math.Max(tailHistory.Min(p => p.X), tailHistory.Min(p => p.X));

            minY = Min(headHistory.Min(p => p.Y), tailHistory.Min(p => p.Y), head.Y, tail.Y);
            maxY = Max(headHistory.Max(p => p.Y), tailHistory.Max(p => p.Y), head.Y, tail.Y);

            minX = Min(headHistory.Min(p => p.X), tailHistory.Min(p => p.X), head.X, tail.X);
            maxX = Max(headHistory.Max(p => p.X), tailHistory.Max(p => p.X), head.X, tail.X);

            grid = MakeGrid(minY, maxY, minX, maxX);

            AddHistoryToGrid(grid, tailHistory, CellState.TailHasVisited);
            UpdateGrid(grid, new Point(0, 0), CellState.Origin);
            UpdateGrid(grid, tail, CellState.Tail);
            UpdateGrid(grid, head, CellState.Head);
        }

        public GridVisualiser(List<Knot> knots, HashSet<Point> tailHistory)
        {
            // discover the largest X and Y coordinates of all the points that have been visited

            minY = knots.Select(k => k.Point.Y)
                        .Concat(tailHistory.Select(p => p.Y))
                        .Min();

            maxY = knots.Select(k => k.Point.Y)
                        .Concat(tailHistory.Select(p => p.Y))
                        .Max();

            minX = knots.Select(k => k.Point.X)
                        .Concat(tailHistory.Select(p => p.X))
                        .Min();

            maxX = knots.Select(k => k.Point.X)
                        .Concat(tailHistory.Select(p => p.X))
                        .Max();

            // ensure the origin point is included
            var origin = new Point(0, 0);

            minY = Math.Min(minY, origin.Y);
            maxY = Math.Max(maxY, origin.Y);

            minX = Math.Min(minX, origin.X);
            maxX = Math.Max(maxX, origin.X);

            grid = MakeGrid(minY, maxY, minX, maxX);

            UpdateGrid(grid, origin, CellState.Origin);

            foreach (var knot in knots.Skip(1).Reverse()) // skip head (the first knot), show Tail 9 first then Tail 1 last which will overwrite cell 0,0
            {
                UpdateGrid(grid, knot.Point, mapId[knot.Id]);
            }

            AddHistoryToGrid(grid, tailHistory, CellState.TailHasVisited);

            UpdateGrid(grid, knots[0].Point, CellState.Head);
        }

        public string GetText()
        {
            var builder = new StringBuilder();

            for (int row = maxY; row >= minY; row--) // top row (highest number) first
            {
                for (int col = minX; col <= maxX; col++)
                {
                    var p = new Point(col, row);
                    var state = grid[p];

                    builder.Append(map[state]);
                }

                builder.AppendLine();
            }

            return builder.ToString();
        }

        private static int Min(int a, int b, int c, int d)
        {
            var items = new List<int>() { a, b, c, d };

            return items.Min();
        }

        private static int Max(int a, int b, int c, int d)
        {
            var items = new List<int>() { a, b, c, d };

            return items.Max();
        }

        private static Dictionary<Point, CellState> MakeGrid(int rowMin, int rowMax, int colMin, int colMax) // inclusive ranges
        {
            var grid = new Dictionary<Point, CellState>();

            for (int row = rowMin; row <= rowMax; row++)
            {
                for (int col = colMin; col <= colMax; col++)
                {
                    var point = new Point(col, row);
                    grid.Add(point, CellState.Empty);
                }
            }

            return grid;
        }

        private static void AddHistoryToGrid(Dictionary<Point, CellState> grid, HashSet<Point> history, CellState state) 
        {
            foreach(var p in history)
            {
                if (!grid.ContainsKey(p))
                {
                    throw new ArgumentOutOfRangeException($"Point {p} not found in grid");
                }

                grid[p] = state;
            }
        }

        private static void UpdateGrid(Dictionary<Point, CellState> grid, Point point, CellState state)
        {
            if (!grid.ContainsKey(point))
            {
                throw new ArgumentOutOfRangeException($"Point {point} not found in grid");
            }

            grid[point] = state;
        }
    }
}
