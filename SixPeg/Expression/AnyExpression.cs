using Pegasus.Common;
using SixPeg.Matchers;
using System.Diagnostics;

namespace SixPeg.Expression
{
    public abstract class AnyExpression : IVisitable, ILexical
    {
        private IMatcher matcher;

        public Cursor EndCursor { get; set; }
        public Cursor StartCursor { get; set; }

        public Grammar Grammar { get; set; }
        public bool Spaced { get; set; } = false;

        [DebuggerStepThrough]
        public IMatcher GetMatcher()
        {
            if (matcher == null)
            {
                matcher = MakeMatcher();
                if (Spaced)
                {
                    matcher.Space = Grammar.Space.GetMatcher();
                }
            }
            return matcher;
        }

        protected abstract IMatcher MakeMatcher();
        public abstract T Accept<T>(IVisitor<T> visitor);
    }
}
