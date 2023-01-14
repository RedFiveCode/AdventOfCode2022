using System.Collections.Generic;

namespace Day3
{
    class Item
    {
        public Item(char id)
        {
            var map = new Dictionary<char, int>();


            for (char ch = 'a'; ch <= 'z'; ch++)
            {
                map[ch] = (int)ch - (int)'a' + 1;
            }

            for (char ch = 'A'; ch <= 'Z'; ch++)
            {
                map[ch] = (int)ch - (int)'A' + 27;
            }

            Id = id;
            Priority = map[id];
        }

        public char Id { get; private set; }

        public int Priority { get; private set; }

        public override string ToString()
        {
            return Id.ToString();
        }

        public override bool Equals(object obj)
        {
            var other = obj as Item;
            if (other == null) return false;

            return this.Id.Equals(other.Id);
        }
        public override int GetHashCode()
        {
            return Id.GetHashCode();
        }
    }
}
