namespace Day8
{
    internal class Map
    {
        private int[,] trees;

        public Map(int[,] trees)
        {
            this.trees = trees;
        }

        public int[,] GetInteriorTrees()
        {
            int rowCount = trees.GetLength(0);
            int colCount = trees.GetLength(1);

            var innerTrees = new int[rowCount - 2, colCount - 2];

            for (int y = 1; y <= rowCount - 2; y++)
            {
                for (int x = 1; x <= colCount - 2; x++)
                {
                    innerTrees[y - 1, x - 1] = trees[y, x];
                }
            }

            return innerTrees;
        }

        public bool IsVisible(int row, int column)
        {
            var treeHeight = trees[row, column];

            var rowData = GetRow(row);
            var rowLeft = rowData.Take(column).ToArray();  
            var rowRight = rowData.Skip(column + 1).ToArray();

            var columnData = GetColumn(column);
            var columnAbove = columnData.Take(row).ToArray();
            var columnBelow = columnData.Skip(row + 1).ToArray();

            return rowLeft.All(n => n < treeHeight) ||
                   rowRight.All(n => n < treeHeight) ||
                   columnAbove.All(n => n < treeHeight) ||
                   columnBelow.All(n => n < treeHeight);   
        }

        public int GetScenicScore(int row, int column) // Part two
        {
            var treeHeight = trees[row, column];

            var rowData = GetRow(row);
            var rowLeft = rowData.Take(column).ToArray();
            var rowRight = rowData.Skip(column + 1).ToArray();

            var columnData = GetColumn(column);
            var columnAbove = columnData.Take(row).ToArray();
            var columnBelow = columnData.Skip(row + 1).ToArray();

            var leftCount = CountWhileFromEnd(rowLeft, treeHeight);
            var rightCount = CountWhileFromStart(rowRight, treeHeight);

            var aboveCount = CountWhileFromEnd(columnAbove, treeHeight);
            var belowCount = CountWhileFromStart(columnBelow, treeHeight);

            return leftCount * rightCount * aboveCount * belowCount;
        }

        public int RowCount
        {
            get { return trees.GetLength(0); }
        }

        public int ColumnCount
        {
            get { return trees.GetLength(1); }
        }

        public int GetBorderCount()
        {
            int rowCount = trees.GetLength(0);
            int colCount = trees.GetLength(1);

            return 2 * (rowCount + colCount - 2);
        }

        private int[] GetRow(int row)
        {
            int colCount = trees.GetLength(1);

            var rowData = new int[colCount];

            for (int col = 0; col < colCount; col++)
            {
                rowData[col] = trees[row, col];
            }

            return rowData;
        }

        private int[] GetColumn(int column)
        {
            int rowCount = trees.GetLength(0);

            var colData = new int[rowCount];

            for (int row = 0; row < rowCount; row++)
            {
                colData[row] = trees[row, column];
            }

            return colData;
        }

        private int CountWhileFromStart(int[] values, int height)
        {
            int count = 0;

            for (int i = 0; i < values.Length; i++)
            {
                count++;

                var current = values[i];

                if (current >= height)
                {
                    break;
                }
            }

            return count;
        }

        private int CountWhileFromEnd(int[] values, int height)
        {
            int count = 0;

            for (int i = values.Length - 1; i >= 0; i--)
            {
                count++;

                var current = values[i];

                if (current >= height)
                {
                    break;
                }
            }

            return count;
        }
    }
}
