using System;
using System.IO;

namespace Six.Support
{
    public class Navi
    {
        protected Navi(DirectoryInfo root, DirectoryInfo project)
        {
            Workspace = root;
            Project = project;
            Temp = new DirectoryInfo(Path.Combine(Workspace.FullName, "Six.Source", "Reports"));
            SixCore = new DirectoryInfo(Path.Combine(Workspace.FullName, "Six.Source", "Core"));
            SixCoreFull = new DirectoryInfo(Path.Combine(Workspace.FullName, "Six.Source", "CoreFull"));
            SixTests = new DirectoryInfo(Path.Combine(Workspace.FullName, "Six.Source", "Tests"));
        }
        protected Navi(string root, string project) : this(new DirectoryInfo(root), new DirectoryInfo(project)) { }

        public Navi() : this("../../../..", "../../..") { }

        public DirectoryInfo Workspace { get; }
        public DirectoryInfo Project { get; }
        public DirectoryInfo Temp { get; }
        public DirectoryInfo SixCore { get; }
        public DirectoryInfo SixCoreFull { get; }
        public DirectoryInfo SixTests { get; }


        public DirectoryInfo TempFor(DirectoryInfo work)
        {
            if (work.FullName.StartsWith(Workspace.FullName))
            {
                return new DirectoryInfo(Path.Combine(Temp.FullName, work.FullName[(Workspace.FullName.Length + 1)..]));
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
