using System.Diagnostics;

namespace SixComp.ParseTree
{
    public interface AnySelfExpression : AnyPrimary
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
