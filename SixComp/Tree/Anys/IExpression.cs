using SixComp.Support;

namespace SixComp
{
    public partial class ParseTree
    {
        public interface IExpression : IWritable
        {
            public static IExpression? TryParse(Parser parser)
            {
                return InfixList.TryParse(parser);
            }

            IExpression? LastExpression { get; }
        }
    }
}
