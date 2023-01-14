using System.Text.RegularExpressions;

namespace Day9
{
    internal class MotionReader
    {
        public static List<Motion> Read(string filename)
        {
            var text = File.ReadAllLines(filename);

            return text.Select(line => ParseLine(line)).ToList();
        }

        private static Motion ParseLine(string line)
        {
            // "R 4"
            var regex = new Regex(@"(?<direction>.) (?<count>\d+)");
            var match = regex.Match(line);

            if (match != null && match.Success)
            {
                var direction = (Motion.Directions)Enum.Parse(typeof(Motion.Directions), match.Groups["direction"].Value);
                var count = Int32.Parse(match.Groups["count"].Value);

                return new Motion(direction, count);
            }

            throw new ArgumentOutOfRangeException($"Invalid line '{line}'");
        }
    }
}