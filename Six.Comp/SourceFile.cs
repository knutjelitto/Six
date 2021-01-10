using System;
using System.IO;

namespace Six.Comp
{
    public class SourceFile
    {
        public SourceFile(string path, string name, int no, bool skip)
        {
            Path = new FileInfo(path.Replace('\\', '/'));
            Name = name;
            No = no;
            Skip = skip;
            Lines = -1;
            Time = TimeSpan.FromSeconds(0);
        }

        public FileInfo Path { get; }
        public string Name { get; }
        public int No { get; }
        public bool Skip { get; }

        public int Lines { get; set; }
        public TimeSpan Time { get; set; }

        public int Lps
        {
            get
            {
                if (!Skip && Time.TotalSeconds > 0)
                {
                    return (int)Math.Round(Lines / Time.TotalSeconds);
                }
                return int.MaxValue;
            }
        }
    }
}
