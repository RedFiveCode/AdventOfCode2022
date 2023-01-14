using System.Collections.Generic;
using System.Linq;

namespace Day7
{
    internal class Folder
    {
        public Folder(string name)
        {
            Name = name;
            Size = 0;
            Files = new List<File>();
            SubFolders = new List<Folder>();
        }
        public string Name { get; private set; }
        public int Size { get; private set; }

        public List<File> Files { get; private set; }
        public List<Folder> SubFolders { get; private set; }

        public void AddFile(File file)
        {
            Files.Add(file);
        }

        public void AddSubfolder(Folder folder)
        {
            SubFolders.Add(folder);
        }

        public bool ContainsSubfolder(string subFolderName)
        {
            return SubFolders.Any(f => f.Name == subFolderName);
        }

        public Folder GetSubFolderOrDefault(string subFolderName)
        {
            return SubFolders.FirstOrDefault(s => s.Name == subFolderName);
        }
        public void CalculateFolderSize()
        {
            Size = GetFolderSize();
        }

        public int GetFolderSize()
        {
            // TODO include any subfolders as well
            int folderSize = SubFolders.Sum(s => s.GetFolderSize());

            int fileSize = Files.Sum(f => f.Size);

            return folderSize + fileSize;
        }

        public List<Folder> GetSubFolders()
        {
            var allSubFolders = new List<Folder>();

            allSubFolders.AddRange(SubFolders);

            foreach (var subFolder in SubFolders)
            {
                allSubFolders.AddRange(subFolder.GetSubFolders());
            }

            return allSubFolders;

        }

        public override string ToString()
        {
            return $"{Name} : (Files: {Files.Count}, Subfolders: {SubFolders.Count}, Size: {Size})";
        }
    }

}
