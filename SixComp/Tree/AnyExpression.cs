using SixComp.Support;

namespace SixComp
{
    public partial class Tree
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
}
