using SixComp.Support;

namespace SixComp
{
    public class Context
    {
        public Context(string filename, string content)
        {
            Source = new Source(filename, content);
            Index = new SourceIndex(Source);
            Lexer = new Lexer(this);
            Tokens = new Tokens(this);
            Parser = new Parser(this);
        }

        public Source Source { get; }
        public SourceIndex Index { get; }
        public Lexer Lexer { get; }
        public Tokens Tokens { get; }
        public Parser Parser { get; }
    }
}
