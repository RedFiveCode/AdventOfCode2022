// See https://aka.ms/new-console-template for more information
using Day8;



//PartOne("Example.txt");
//PartOne("Data.txt");

//PartTwo("Example.txt");
PartTwo("Data.txt");


static void PartOne(string filename)
{
    var trees = MapReader.Read(filename);

    var map = new Map(trees);
    int visibleCount = 0;

    for (int row = 1; row <= map.RowCount - 2; row++)
    {
        for (int col = 1; col <= map.ColumnCount - 2; col++)
        {
            var isTreeVisible = map.IsVisible(row, col);
            //Console.WriteLine($"{row} {col} {isTreeVisible}");

            Console.Write($"{(isTreeVisible ? "T" : ".")}");

            if (isTreeVisible)
            {
                visibleCount++;
            }
        }
        Console.WriteLine();
    }

    Console.WriteLine($"Border : {map.GetBorderCount()}");                //  392
    Console.WriteLine($"Visible: {visibleCount}");                        // 1387
    Console.WriteLine($"Total  : {visibleCount + map.GetBorderCount()}"); // 1779
}

static void PartTwo(string filename)
{
    var trees = MapReader.Read(filename);

    var map = new Map(trees);

    //var s = map.GetScenicScore(3, 2);
    
    int maxScenicScore = 0;

    for (int row = 1; row <= map.RowCount - 2; row++)
    {
        for (int col = 1; col <= map.ColumnCount - 2; col++)
        {
            var score = map.GetScenicScore(row, col);

            if (score > maxScenicScore)
            {
                maxScenicScore = score;
            }
        }
        Console.WriteLine();
    }

    Console.WriteLine($"Maximum scenic score : {maxScenicScore}"); // 172224
}


