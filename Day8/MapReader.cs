
namespace Day8
{
    internal class MapReader
    {
        public static int[,] Read(string filename)
        {           
            var text = File.ReadAllLines(filename);

            int rowCount = text.Length;
            int colCount = text[0].Length;

            var trees = new int[rowCount, colCount]; // y, x


            for (int row = 0; row < rowCount; row++)
            {
                var currentLine = text[row];

                for (int col = 0; col < colCount; col++)
                {
                    var currentColumn = currentLine[col];

                    if (currentColumn >= '0' && currentColumn <= '9')
                    {
                        trees[row, col] = Int32.Parse(new string(currentColumn, 1));
                    }
                }
            }

            return trees;
        }
    }
}
