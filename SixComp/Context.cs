using SixComp.Support;
using System.IO;

namespace SixComp
{
    public class Context
    {
        public Context(FileInfo file, DirectoryInfo temp, string content)
        {
            File = file;
            Temp = temp;
            Source = new Source(file.FullName, content);
            Index = new SourceIndex(Source);
            Tokens = new Tokens(this);
            Lexer = new Lexer(this);
            Parser = new Parser(this);
        }

        public FileInfo File { get; }
        public DirectoryInfo Temp { get; }

        public Source Source { get; }
        public SourceIndex Index { get; }
        public Lexer Lexer { get; }
        public Tokens Tokens { get; }
        public Parser Parser { get; }

        public string Short => Path.GetFileNameWithoutExtension(File.Name);
    }
}
