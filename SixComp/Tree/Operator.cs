using SixComp.Common;
using SixComp.Support;
using System;

namespace SixComp
{
    public partial class ParseTree
    {
        public abstract class Operator : SyntaxNode
        {
            public static Operator From(Token op)
            {
                return new NamedOperator(op);
            }

            public static Operator Assignment(Token op)
            {
                return new AssignmentOperator(op);
            }

            public static Operator From(IExpression middle)
            {
                return new ConditionalOperator(middle);
            }

            public class CastOperator : Operator
            {
                public static readonly TokenSet Firsts = new TokenSet(ToKind.KwAs, ToKind.KwIs);

                public CastOperator(CastKind kind)
                {
                    Kind = kind;
                }

                public CastKind Kind { get; }

                public static CastOperator Parse(Parser parser)
                {
                    if (parser.Match(ToKind.KwIs))
                    {
                        return new CastOperator(CastKind.Is);
                    }
                    if (parser.Match(ToKind.KwAs))
                    {
                        if (parser.Match(ToKind.Bang))
                        {
                            return new CastOperator(CastKind.AsForce);
                        }
                        if (parser.Match(ToKind.Quest))
                        {
                            return new CastOperator(CastKind.AsChain);
                        }
                        return new CastOperator(CastKind.As);
                    }

                    parser.Consume(Firsts);

                    throw new InvalidOperationException("<NEVER>");
                }

                public override string ToString()
                {
                    return Kind switch
                    {
                        CastKind.Is => "is",
                        CastKind.As => "as",
                        CastKind.AsForce => "as!",
                        CastKind.AsChain => "as?",
                        _ => throw new InvalidOperationException(),
                    };
                }
            }

            public class AssignmentOperator : Operator
            {
                public AssignmentOperator(Token @operator)
                {
                    Operator = @operator;
                    Name = BaseName.From(Operator);
                }

                public Token Operator { get; }
                public BaseName Name { get; }

                public override string ToString()
                {
                    return $"{Operator}";
                }
            }

            public class NamedOperator : Operator
            {
                public NamedOperator(Token @operator)
                {
                    Operator = @operator;
                    Name = BaseName.From(Operator);
                }

                public Token Operator { get; }
                public BaseName Name { get; }

                public override string ToString()
                {
                    return $"{Operator}";
                }
            }

            public class ConditionalOperator : Operator
            {
                public ConditionalOperator(IExpression middle)
                {
                    Middle = middle;
                }

                public IExpression Middle { get; }

                public override string ToString()
                {
                    return $"? {Middle} :";
                }
            }
        }
    }
}