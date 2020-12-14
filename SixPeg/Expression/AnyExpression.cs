using Pegasus.Common;
using SixPeg.Matchers;

namespace SixPeg.Expression
{
    public abstract class AnyExpression : ILexical
    {
        public Cursor EndCursor { get; set; }
        public Cursor StartCursor { get; set; }

        public abstract void Resolve(GrammarExpression grammar);
        public abstract IMatcher GetMatcher(bool spaced);
    }
}
