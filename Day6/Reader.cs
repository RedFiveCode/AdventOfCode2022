using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day6
{

    internal class Reader
    {
        public static List<Datastream> Read(string filename)
        {
            var result = new List<Datastream>();

            var lines = File.ReadAllLines(filename);    

            return lines.Select(l => new Datastream(l)).ToList();
        }
    }
}
