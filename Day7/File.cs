namespace Day7
{
    internal class File
    {
        public File(string name, int size)
        {
            Name = name;
            Size = size;
        }

        public string Name { get; private set; }
        public int Size { get; private set; }

        public override string ToString()
        {
            return $"{Name} : {Size}";
        }
    }

}
