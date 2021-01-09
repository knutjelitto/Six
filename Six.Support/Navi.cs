using System;
using System.IO;

namespace Six.Support
{
    public class Navi
    {
        protected Navi(DirectoryInfo project)
        {
            Project = project;

            Temp = new DirectoryInfo(Path.Combine(Projects.FullName, "Temp"));

            SwiftCore = new DirectoryInfo(Path.Combine(Workspace.FullName, "Six.Source", "Swift", "Core"));
            SwiftCoreFull = new DirectoryInfo(Path.Combine(Workspace.FullName, "Six.Source", "Swift", "CoreFull"));
            SwiftTests = new DirectoryInfo(Path.Combine(Workspace.FullName, "Six.Source", "Swift", "Tests"));
        }
        protected Navi(string project) : this(new DirectoryInfo(project)) { }

        public Navi() : this("../../..") { }

        public DirectoryInfo Project { get; }

        public DirectoryInfo Workspace => Project.Parent;
        public DirectoryInfo Projects => Workspace.Parent;
        public DirectoryInfo Temp { get; }
        public DirectoryInfo SwiftCore { get; }
        public DirectoryInfo SwiftCoreFull { get; }
        public DirectoryInfo SwiftTests { get; }

        public DirectoryInfo GetProject(string projectName)
        {
            return new DirectoryInfo(Path.Combine(Workspace.FullName, projectName));
        }

        public DirectoryInfo Subdir(DirectoryInfo parent, string name)
        {
            return new DirectoryInfo(Path.Combine(parent.FullName, name));
        }

        public FileInfo File(DirectoryInfo parent, string name)
        {
            return new FileInfo(Path.Combine(parent.FullName, name));
        }


        public DirectoryInfo TempFor(DirectoryInfo work)
        {
            if (work.FullName.StartsWith(Projects.FullName))
            {
                var directory = new DirectoryInfo(Path.Combine(Temp.FullName, work.FullName[(Projects.FullName.Length + 1)..]));
                directory.Create();
                return directory;
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
