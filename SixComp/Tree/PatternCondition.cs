using SixComp.Support;
using System;

namespace SixComp
{
    public partial class ParseTree
    {
        public abstract class PatternCondition : BaseExpression, ICondition
        {
            public static readonly TokenSet Firsts = new TokenSet(ToKind.KwCase, ToKind.KwLet, ToKind.KwVar);

            protected PatternCondition(IPattern pattern, IType? type, Initializer initializer)
            {
                Pattern = pattern;
                Type = type;
                Initializer = initializer;
            }

            public IPattern Pattern { get; }
            public IType? Type { get; }
            public Initializer Initializer { get; }

            public override IExpression? LastExpression => Initializer.LastExpression;

            public static PatternCondition Parse(Parser parser)
            {
                var first = parser.Consume(Firsts);

                var pattern = IPattern.Parse(parser);
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
                public LetPatternCondition(IPattern pattern, IType? type, Initializer initializer)
                    : base(pattern, type, initializer)
                {
                }
            }

            public class VarPatternCondition : PatternCondition
            {
                public VarPatternCondition(IPattern pattern, IType? type, Initializer initializer)
                    : base(pattern, type, initializer)
                {
                }
            }

            public class CasePatternCondition : PatternCondition
            {
                public CasePatternCondition(IPattern pattern, IType? type, Initializer initializer)
                    : base(pattern, type, initializer)
                {
                }
            }
        }
    }
}