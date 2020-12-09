using SixComp.Support;
using System;

namespace SixComp
{
    public partial class Tree
    {
        public abstract class PatternCondition : BaseExpression, AnyCondition
        {
            public static readonly TokenSet Firsts = new TokenSet(ToKind.KwCase, ToKind.KwLet, ToKind.KwVar);

            protected PatternCondition(AnyPattern pattern, AnyType? type, Initializer initializer)
            {
                Pattern = pattern;
                Type = type;
                Initializer = initializer;
            }

            public AnyPattern Pattern { get; }
            public AnyType? Type { get; }
            public Initializer Initializer { get; }

            public override AnyExpression? LastExpression => Initializer.LastExpression;

            public static PatternCondition Parse(Parser parser)
            {
                var first = parser.Consume(Firsts);

                var pattern = AnyPattern.Parse(parser);
                var type = parser.Try(TypeAnnotation.Firsts, TypeAnnotation.Parse);
                var init = Initializer.Parse(parser);

                return first.Kind switch
                {
                    ToKind.KwLet => new LetPatternCondition(pattern, type, init),
                    ToKind.KwVar => new VarPatternCondition(pattern, type, init),
                    ToKind.KwCase => new CasePatternCondition(pattern, type, init),
                    _ => throw new InvalidOperationException(),
                };
            }

            public override string ToString()
            {
                return $"{Pattern}{Type}{Initializer}";
            }

            public class LetPatternCondition : PatternCondition
            {
                public LetPatternCondition(AnyPattern pattern, AnyType? type, Initializer initializer)
                    : base(pattern, type, initializer)
                {
                }
            }

            public class VarPatternCondition : PatternCondition
            {
                public VarPatternCondition(AnyPattern pattern, AnyType? type, Initializer initializer)
                    : base(pattern, type, initializer)
                {
                }
            }

            public class CasePatternCondition : PatternCondition
            {
                public CasePatternCondition(AnyPattern pattern, AnyType? type, Initializer initializer)
                    : base(pattern, type, initializer)
                {
                }
            }
        }
    }
}