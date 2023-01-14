using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day10
{
    internal class Program
    {
        static void Main(string[] args)
        {
            PartOne();
        }

        static void PartOne()
        {
            //var instructions = Reader.Read("Example.txt");
            //var instructions = Reader.Read("Example2.txt");
            var instructions = Reader.Read("Data.txt");

            var processor = new InstructionProcessor();

            processor.Run(instructions);

        }
    }
}