using SixComp.Support;
using System;
using System.Diagnostics.Contracts;

namespace SixComp.ParseTree
{
    public abstract class Operator : SyntaxNode
    {
        public enum CastKind
        {
            Is,
            As,
            AsForce,
            AsChain,
        }

        public static Operator From(Token op)
        {
            return new TokenOperator(op);
        }

        public static Operator From(AnyExpression middle)
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

                throw new InvalidOperationException();
            }
        }

        public class TokenOperator : Operator
        {
            public TokenOperator(Token @operator)
            {
                Operator = @operator;
            }

            public Token Operator { get; }

            public override string ToString()
            {
                return $"{Operator}";
            }
        }

        public class ConditionalOperator : Operator
        {
            public ConditionalOperator(AnyExpression middle)
            {
                Middle = middle;
            }

            public AnyExpression Middle { get; }

            public override string ToString()
            {
                return $"? {Middle} :";
            }
        }
    }
}
