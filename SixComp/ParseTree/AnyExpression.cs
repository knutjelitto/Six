using SixComp.Support;

namespace SixComp.ParseTree
{
    public interface AnyExpression : IWriteable
    {
        public static AnyExpression Parse(Parser parser)
        {
            return parser.ParseExpression();
        }

        public static AnyExpression Parse(Parser parser, int precedence)
        {
            return parser.ParseExpression(precedence);
        }
    }
}
