namespace Day9
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //PartOne("Example.txt"); // 13
            //PartOne("Data.txt"); // 6098

            //PartTwo("Example.txt"); // 13
            //PartTwo("ExamplePartTwo.txt"); // 36
            PartTwo("Data.txt"); //2597
        }

        static void PartOne(string filename)
        {
            var motions = MotionReader.Read(filename);

            var navigator = new RopeNavigatorPartOne();

            foreach (var motion in motions)
            {
                navigator.Move(motion);
            }

            Console.WriteLine("Tail Visited Count: {0}", navigator.TailCount); // 6098
        }

        static void PartTwo(string filename)
        {
            var motions = MotionReader.Read(filename);

            var navigator = new RopeNavigator(10); // 10 knots = head and 9 knots
            //var navigator = new RopeNavigator(2);

            Console.WriteLine(navigator.ShowGrid());
            navigator.Move(motions);

            Console.WriteLine(navigator.ShowGrid());
            Console.WriteLine("Tail Visited Count: {0}", navigator.TailCount);
        }

        static void MainOld(string[] args)
        {
            var motions = MotionReader.Read("Example.txt");
            //var motions = MotionReader.Read("Data.txt");

            var grid = new Grid();

            Console.WriteLine(grid.GetText());
            //Console.WriteLine($"Distance = {navigator.HeadTailDistance}");

            foreach (var motion in motions)
            {
                grid.Move(motion);

                Console.WriteLine(grid.GetText());
            }

            Console.WriteLine("Tail Visited Count: {0}", grid.GetTailVisitedCount());
        }
    }
}