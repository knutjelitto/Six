﻿using System;
using System.IO;

namespace SixComp
{
    public class Navi
    {
        public Navi(DirectoryInfo root)
        {
            Root = root;
            SixCore = new DirectoryInfo(Path.Combine(Root.FullName, "Six.Core"));
            Temp = new DirectoryInfo(Path.Combine(Root.FullName, "../Temp/Six"));
            SwiftSources = new DirectoryInfo(Path.Combine(Root.FullName, "../Swift"));
        }

        public Navi(string root) : this(new DirectoryInfo(root)) { }

        public Navi() : this("../../../..") { }

        public DirectoryInfo Root { get; }
        public DirectoryInfo SixCore { get; }
        public DirectoryInfo Temp { get; }
        public DirectoryInfo SwiftSources { get; }

        public DirectoryInfo TempFor(DirectoryInfo work)
        {
            if (work.FullName.StartsWith(Root.FullName))
            {
                return new DirectoryInfo(Path.Combine(Temp.FullName, work.FullName.Substring(Root.FullName.Length + 1)));
            }
            throw new ArgumentOutOfRangeException(nameof(work));
        }

        public DirectoryInfo TempFor(string work)
        {
            return new DirectoryInfo(Path.Combine(Temp.FullName, work));
        }

        public FileInfo FileIn(DirectoryInfo directory, string name)
        {
            return new FileInfo(Path.Combine(directory.FullName, name));
        }
    }
}
