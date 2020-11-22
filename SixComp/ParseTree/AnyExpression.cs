using SixComp.Support;

namespace SixComp.ParseTree
{
    public interface AnyExpression : IWritable
    {
        public static AnyExpression? TryParse(Parser parser)
        {
            return parser.TryParseExpression(0);
        }

        public static AnyExpression? TryParse(Parser parser, int precedence)
        {
            return parser.TryParseExpression(precedence);
        }

        AnyExpression? LastExpression { get; }
    }
}
