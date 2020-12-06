using SixComp.Support;

namespace SixComp.Tree
{
    public interface AnyExpression : IWritable
    {
        public static AnyExpression? TryParse(Parser parser)
        {
            return InfixList.TryParse(parser);
        }

        AnyExpression? LastExpression { get; }
    }
}
