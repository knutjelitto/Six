using SixComp.Support;

namespace SixComp
{
    public partial class Tree
    {
        public class GenericParameterClause : IWritable
        {
            public static readonly TokenSet Firsts = new TokenSet(ToKind.Less);

            public GenericParameterClause(GenericParameterList parameters)
            {
                Parameters = parameters;
            }

            public GenericParameterClause() : this(new GenericParameterList()) { }

            public GenericParameterList Parameters { get; }

            public static GenericParameterClause Parse(Parser parser)
            {
                parser.Consume(ToKind.Less);

                var parameters = GenericParameterList.Parse(parser);

                parser.ConsumeCarefully(ToKind.Greater);

                return new GenericParameterClause(parameters);
            }

            public override string ToString()
            {
                return Parameters.Missing ? string.Empty : $"<{Parameters}>";
            }
        }
    }
}