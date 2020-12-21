using Six.Support;
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
            Tokens = new Tokens(this);
            Lexer = new Lexer(this);
            Parser = new Parser(this);
            Error = new Error(Source);
        }

        public FileInfo File { get; }
        public DirectoryInfo Temp { get; }

        public Source Source { get; }
        public SourceIndex Index => Source.Index;
        public Lexer Lexer { get; }
        public Tokens Tokens { get; }
        public Parser Parser { get; }
        public Error Error { get; }

        public string Short => Path.GetFileNameWithoutExtension(File.Name);
    }
}
