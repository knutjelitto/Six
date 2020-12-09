using System.Diagnostics;

namespace SixComp
{
    public partial class Tree
    {
        public interface AnySelfExpression : AnyPrimaryExpression
        {
            public static AnySelfExpression Parse(Parser parser)
            {
                Debug.Assert(parser.Current == ToKind.KwSelf);

                if (parser.Next == ToKind.LBracket)
                {
                    return SelfSubscriptExpression.Parse(parser);
                }

                if (parser.Next == ToKind.Dot)
                {
                    if (parser.NextNext == ToKind.KwInit)
                    {
                        return SelfInitExpression.Parse(parser);
                    }

                    return SelfMethodExpression.Parse(parser);
                }

                return SelfExpression.Parse(parser);
            }
        }
    }
}