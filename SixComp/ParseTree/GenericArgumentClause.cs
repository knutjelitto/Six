using SixComp.Support;

namespace SixComp.ParseTree
{
    public class GenericArgumentClause : IWriteable
    {
        public GenericArgumentClause(GenericArgumentList arguments)
        {
            Arguments = arguments;
        }

        public GenericArgumentClause()
            : this(new GenericArgumentList())
        {
        }

        public GenericArgumentList Arguments { get; }

        public static GenericArgumentClause Parse(Parser parser)
        {
            parser.Consume(ToKind.Less);

            var arguments = GenericArgumentList.Parse(parser);

            parser.Consume(ToKind.Greater);

            return new GenericArgumentClause(arguments);
        }

        public override string ToString()
        {
            return Arguments.Missing ? string.Empty : $"<{Arguments}>";
        }
    }
}
