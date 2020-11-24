using SixComp.Support;

namespace SixComp
{
    public class Context
    {
        public Context(string filename, string content)
        {
            Source = new Source(filename, content);
            Index = new SourceIndex(Source);
            Tokens = new Tokens(this);
            Lexer = new Lexer(this);
            Parser = new Parser(this);
        }

        public Source Source { get; }
        public SourceIndex Index { get; }
        public Lexer Lexer { get; }
        public Tokens Tokens { get; }
        public Parser Parser { get; }
    }
}
