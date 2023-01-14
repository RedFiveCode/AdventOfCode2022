using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Day6
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //PartOne();
            PartTwo();
        }

        private static void PartOne()
        {           
            //var messages = Reader.Read("Example.txt");
            var messages = Reader.Read("Data.txt");

            int sum = 0;

            foreach (var message in messages)
            {
                int index = message.GetStartOfPacketMarker();

                Console.WriteLine($"{message.Message} : {index}");

                sum += index;
            }

            Console.WriteLine($"Sum {sum}"); // 1965
        }

        private static void PartTwo()
        {
            //var messages = Reader.Read("Example.txt");
            var messages = Reader.Read("Data.txt");

            int sumPacket = 0;
            int sumMessage = 0;

            foreach (var message in messages)
            {
                int indexPacket = message.GetStartOfPacketMarker();
                int indexMessage = message.GetStartOfMessageMarker();

                Console.WriteLine($"{message.Message} : {indexPacket}, {indexMessage}");

                sumPacket += indexPacket;
                sumMessage += indexMessage;
            }

            Console.WriteLine($"Sum packet={sumPacket}, message={sumMessage}"); // 2773
        }
    }
}
