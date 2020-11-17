using System;

namespace SixComp.ParseTree
{
    public interface AnyPrimary : AnyExpression
    {
        public static new AnyPrimary Parse(Parser parser)
        {
            switch (parser.Current.Kind)
            {
                case ToKind.Number:
                    return NumberLiteralExpression.Parse(parser);
                case ToKind.String:
                    return StringLiteralExpression.Parse(parser);
                case ToKind.Name:
                    return NameExpression.Parse(parser);
                case ToKind.KwSelf:
                    return SelfExpression.Parse(parser);
                case ToKind.LBracket:
                    return ArrayLiteral.Parse(parser);
                case ToKind.Dot:
                    return ImplicitMemberExpression.Parse(parser);
                case ToKind.KwLet:
                case ToKind.KwVar:
                    return ExpressionPattern.Parse(parser);
            }


            throw new InvalidOperationException($"couldn't continue on {parser.Current.Kind}");
        }
    }
}
