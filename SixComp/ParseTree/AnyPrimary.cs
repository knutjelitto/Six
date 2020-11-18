using System;
using System.Diagnostics;
using SixComp.Support;

namespace SixComp.ParseTree
{
    public interface AnyPrimary : AnyExpression
    {
        public static new AnyPrimary Parse(Parser parser)
        {
            switch (parser.Current)
            {
                case ToKind.Number:
                    return NumberLiteralExpression.Parse(parser);
                case ToKind.String:
                    return StringLiteralExpression.Parse(parser);
                case ToKind.Name:
                    var name = NameExpression.Parse(parser);
                    if (name.ToString() == "isFinite")
                    {
                        Debug.Assert(true);
                    }
                    return name;
                case ToKind.KwSelf:
                    return AnySelfExpression.Parse(parser);
                case ToKind.LParent:
                    return NestedExpression.Parse(parser);
                case ToKind.LBracket:
                    return ArrayLiteral.Parse(parser);
                case ToKind.Dot:
                    return ImplicitMemberExpression.Parse(parser);
                case ToKind.KwFalse:
                case ToKind.KwTrue:
                    return BoolLiteralExpression.Parse(parser);
                case ToKind.KwNil:
                    return NilLiteralExpression.Parse(parser);
            }


            throw new InvalidOperationException($"couldn't continue on `{parser.Current.GetRep()}`");
        }
    }
}
