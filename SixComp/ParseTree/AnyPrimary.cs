using System;
using System.Collections.Generic;
using SixComp.Support;

namespace SixComp.ParseTree
{
    public interface AnyPrimary : AnyExpression
    {
        private static TokenSet Firsts = new TokenSet(
            ToKind.Number, ToKind.String, ToKind.Name, ToKind.KwSelf, ToKind.LParent, ToKind.LBracket, ToKind.Dot, ToKind.KwFalse, ToKind.KwTrue,
            ToKind.KwNil, ToKind.Backslash);

        public static new AnyPrimary Parse(Parser parser)
        {
            switch (parser.Current)
            {
                case ToKind.Number:
                    return NumberLiteralExpression.Parse(parser);
                case ToKind.String:
                    return StringLiteralExpression.Parse(parser);
                case ToKind.Name:
                    return NameExpression.Parse(parser);
                case ToKind.KwSelf:
                    return AnySelfExpression.Parse(parser);
                case ToKind.LParent:
                    return NestedOrTuple(parser);
                case ToKind.LBracket:
                    return ArrayLiteral.Parse(parser);
                case ToKind.Dot:
                    return ImplicitMemberExpression.Parse(parser);
                case ToKind.KwFalse:
                case ToKind.KwTrue:
                    return BoolLiteralExpression.Parse(parser);
                case ToKind.KwNil:
                    return NilLiteralExpression.Parse(parser);
                case ToKind.Backslash:
                    return KeyPathExpression.Parse(parser);
            }

            parser.Consume(Firsts);

            throw new NotSupportedException();
        }

        private static AnyPrimary NestedOrTuple(Parser parser)
        {
            parser.Consume(ToKind.LParent);

            var expressions = new List<AnyExpression>();
            if (parser.Current != ToKind.RParent)
            {
                do
                {
                    var expression = AnyExpression.Parse(parser);
                    expressions.Add(expression);
                }
                while (parser.Match(ToKind.Comma));
            }

            parser.Consume(ToKind.RParent);

            if (expressions.Count == 1)
            {
                return NestedExpression.From(expressions[0]);
            }

            return TupleExpression.From(new ExpressionList(expressions));
        }
    }
}
