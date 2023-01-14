using System.Diagnostics;
using System.IO;
using System.Linq;

namespace Day6
{
    [DebuggerDisplay("{Message}")]
    internal class Datastream
    {
        private const int PacketMarkerLength = 4;
        private const int MessageMarkerLength = 14;

        public Datastream(string text)
        {
            if (text.Length < PacketMarkerLength)
            {
                throw new InvalidDataException($"Packet too short; need at least {PacketMarkerLength} characters");
            }

            Message = text; 
        }

        public string Message { get; private set; }


        public int GetStartOfPacketMarker()
        {
            // Four characters that are all different
            //
            // Identify the first position where the four most recently received characters were all different.
            // Specifically, it needs to report the number of characters from the beginning of the buffer
            // to the end of the first such four-character marker

            for (int i = 0; i < Message.Length - PacketMarkerLength; i++)
            {
                var subString = GetSubstring(i, PacketMarkerLength);

                if (!IsUnique(subString))
                {
                    return i + PacketMarkerLength; // index of message after first start-of-packet marker is found
                }

            }

            return -1; // error
        }

        // Part two
        public int GetStartOfMessageMarker()
        {
            // A start-of-message marker is just like a start-of-packet marker,
            // except it consists of 14 distinct characters rather than 4.

            for (int i = 0; i < Message.Length - MessageMarkerLength; i++)
            {
                var subString = GetSubstring(i, MessageMarkerLength);

                if (!IsUnique(subString))
                {
                    return i + MessageMarkerLength; // index of message after first start-of-message marker is found
                }

            }

            return -1; // error
        }

        private string GetSubstring(int index, int length)
        {
            if (index > Message.Length - 1)
            {
                throw new InvalidDataException($"Index {index} is invalid");
            }

            if (index + length > Message.Length)
            {
                throw new InvalidDataException($"Index {index} and length {length} is larger than the message length ({Message.Length})");
            }

            return Message.Substring(index, length);
        }

        /// <summary>
        /// Returns true if all the characters in the string ar unique,
        /// that is not duplicated with other charatse in the string.
        /// </summary>
        private static bool IsUnique(string s)
        {
            bool anyDuplicates = s.ToCharArray().Distinct().Count() != s.Length;

            return anyDuplicates;
        }
    }
}
