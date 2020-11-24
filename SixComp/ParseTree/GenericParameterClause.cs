using SixComp.Support;

namespace SixComp.ParseTree
{
    public class GenericParameterClause : IWritable
    {
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

            parser.CarefullyConsume(ToKind.Greater);

            return new GenericParameterClause(parameters);
        }

        public override string ToString()
        {
            return Parameters.Missing ? string.Empty : $"<{Parameters}>";
        }
    }
}
