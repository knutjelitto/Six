using SixComp.Support;

namespace SixComp.ParseTree
{
    public class ArgumentClause
    {
        public static readonly TokenSet Firsts = new TokenSet(ToKind.LParent);

        public ArgumentClause(ArgumentList arguments)
        {
            Arguments = arguments;
        }

        public ArgumentClause() : this(new ArgumentList()) { }

        public ArgumentList Arguments { get; }
        public bool Missing => Arguments.Missing;

        public static ArgumentClause Parse(Parser parser)
        {
            parser.Consume(ToKind.LParent);

            var arguments = ArgumentList.Parse(parser);

            parser.Consume(ToKind.RParent);

            return new ArgumentClause(arguments);
        }

        public override string ToString()
        {
            return Arguments.Missing ? string.Empty : $"({Arguments})";
        }
    }
}
