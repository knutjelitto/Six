using System.Diagnostics;

namespace SixComp
{
    public partial class ParseTree
    {
        public class BinaryExpression : SyntaxNode
        {
            private BinaryExpression(Operator @operator, IExpression right)
            {
                Operator = @operator;
                Right = right;
            }

            public Operator Operator { get; }
            public IExpression Right { get; }

            public static BinaryExpression? TryParse(Parser parser)
            {
                var offset = parser.Offset;

                if (parser.CurrentToken.Text == "?")
                {
                    Debug.Assert(true);
                }

                if (parser.IsInfixOperator())
                {
                    var op = parser.ConsumeAny();

                    if (op.Kind == ToKind.Quest)
                    {
                        var middle = InfixList.TryParse(parser);
                        if (middle != null)
                        {
                            if (parser.Match(ToKind.Colon))
                            {
                                var right = InfixList.TryParse(parser, false);
                                if (right != null)
                                {
                                    return new BinaryExpression(Operator.From(middle), right);
                                }
                            }
                        }
                    }
                    else if (op.Kind == ToKind.Assign)
                    {
                        var right = InfixList.TryParse(parser, withBinaries: false);
                        if (right != null)
                        {
                            return new BinaryExpression(Operator.Assignment(op), right);
                        }
                    }
                    else
                    {
                        var right = InfixList.TryParse(parser, withBinaries: false);
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
}