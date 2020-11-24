using SixComp.Support;

namespace SixComp.ParseTree
{
    public interface AnyExpression : IWritable
    {
        public static AnyExpression? TryParse(Parser parser)
        {
            return Expression.TryParse(parser);
        }

        AnyExpression? LastExpression { get; }
    }
}
