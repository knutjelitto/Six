using System;
using System.Diagnostics;
using System.Runtime.Serialization;

namespace SixComp.ParseTree
{
    public class BinaryExpression: SyntaxNode
    {
        private BinaryExpression(Operator @operator, AnyExpression right)
        {
            Operator = @operator;
            Right = right;
        }

        public Operator Operator { get; }
        public AnyExpression Right { get; }

        public static BinaryExpression? TryParse(Parser parser)
        {
            var offset = parser.Offset;

            if (parser.IsInfixOperator())
            {

                var op = parser.ConsumeAny();

                if (op.Kind == ToKind.Quest)
                {
                    var middle = Expression.TryParse(parser);
                    if (middle != null)
                    {
                        if (parser.Match(ToKind.Colon))
                        {
                            var right = Expression.TryParse(parser, false);
                            if (right != null)
                            {
                                return new BinaryExpression(Operator.From(middle), right);
                            }
                        }
                    }
                }
                else
                {
                    var right = AnyPrefixExpression.TryParse(parser);

                    if (right != null)
                    {
                        return new BinaryExpression(Operator.From(op), right);
                    }
                }
            }
            else if (parser.Current == ToKind.KwIs || parser.Current == ToKind.KwAs)
            {
                var @operator = Operator.CastOperator.Parse(parser);
                var right = TypeExpression.Parse(parser);

                return new BinaryExpression(@operator, right);
            }


            parser.Offset = offset;
            return null;
        }

        public override string ToString()
        {
            return $"{Operator} {Right}";
        }
    }
}
