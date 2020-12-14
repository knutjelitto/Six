using Six.Support;

namespace SixComp
{
    public partial class ParseTree
    {
        public class GenericArgumentClause : IWritable
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
            public bool Missing => Arguments.Missing;

            public static GenericArgumentClause? TryParse(Parser parser)
            {
                var offset = parser.Offset;

                if (parser.Match(ToKind.Less))
                {
                    var arguments = GenericArgumentList.Parse(parser);

                    if (parser.ConsumeCarefully(ToKind.Greater) != null)
                    {
                        return new GenericArgumentClause(arguments);

                    }
                }

                parser.Offset = offset;
                return null;
            }

            public override string ToString()
            {
                return Arguments.Missing ? string.Empty : $"<{Arguments}>";
            }
        }
    }
}