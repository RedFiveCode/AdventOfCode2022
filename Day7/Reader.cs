using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace Day7
{
    internal class Reader
    {
        private const string CommandList = "$ ls";
        private const string CommandChangeDirectory = "$ cd";

        private enum ParseState { None = 0, ChangeDirectory = 1, List = 2 }


        public Folder Read(string dataFilename)
        {
            var lines = System.IO.File.ReadAllLines(dataFilename);

            return Parse(lines);
        }

        public Folder Parse(string[] lines)
        {
            var currentState = ParseState.None;
            Stack<Folder> folderStack = new Stack<Folder>();

            Folder parentFolder = null;
            Folder currentFolder = null;

            foreach (var line in lines)
            {
                if (line.StartsWith(CommandChangeDirectory))
                {
                    // handle cd
                    currentState = ParseState.ChangeDirectory;

                    var folderName = OnCommandChangeDirectory(line);
                    if (folderName == "..")
                    {
                        // TODO
                        folderStack.Pop();
                        currentFolder = folderStack.Peek();
                    }
                    else
                    {
                        // get the subfolder object
                        Folder folder = null;

                        if (currentFolder == null) // root
                        {
                            folder = new Folder(folderName);
                            parentFolder = folder;
                            currentFolder = folder;
                        }
                        else
                        {
                            folder = currentFolder.GetSubFolderOrDefault(folderName);

                            if (folderName == null)
                            {
                                throw new ArgumentException($"Subfolder '{folderName}' not found");
                            }

                        }

                        currentFolder = folder;

                        folderStack.Push(currentFolder);
                    }
                }
                else if (line.StartsWith(CommandList))
                {
                    // Handle ls
                    currentState = ParseState.List;
                }
                else
                {
                    if (currentState == ParseState.List)
                    {
                        // entry is file or subfolder in current ls command results

                        // "19116 f" etc
                        var file = ParseFile(line);

                        if (file != null)
                        {
                            // add file to current folder
                            currentFolder.AddFile(file);
                        }
                        else 
                        {
                            // subfolder
                            // add subfolder to current folder
                            // (don't yet know the contents of this subfolder)
                            var folder = ParseFolder(line);
                            currentFolder.AddSubfolder(folder);
                        }
                    }
                    else
                    {
                        throw new ArgumentOutOfRangeException($"Unsupported command '{line}'");
                    }

                }

            }

            return parentFolder;
        }

        private string OnCommandChangeDirectory(string line)
        {
            // "cd subfoldername"
            var regex = new Regex("cd (.*)");
            var match = regex.Match(line);

            // get argument
            if (match.Success)
            {
                return match.Groups[1].Value;   
            }

            throw new ArgumentOutOfRangeException($"Unsupported command parameters '{line}'");
        }

        private File ParseFile(string line)
        {
            var regex = new Regex(@"(\d+) (.*)");

            if (regex.Match(line).Success)
            {
                var size = Int32.Parse(regex.Match(line).Groups[1].Value);
                var name = regex.Match(line).Groups[2].Value;

                return new File(name, size);
            }
            else
            {
                return null;
            }
        }

        private Folder ParseFolder(string line)
        {
            var regex = new Regex(@"dir (.*)");

            if (regex.Match(line).Success)
            {
                var name = regex.Match(line).Groups[1].Value;

                return new Folder(name);
            }
            else
            {
                return null;
            }
        }
    }
}
