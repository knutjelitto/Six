using Pegasus.Common;
using SixPeg.Matchers;

namespace SixPeg.Expression
{
    public abstract class AnyExpression : ILexical
    {
        private IMatcher matcher;

        public Cursor EndCursor { get; set; }
        public Cursor StartCursor { get; set; }

        public GrammarExpression Grammar { get; set; }
        public bool Spaced { get; set; } = false;


        public AnyExpression Resolve(GrammarExpression grammar)
        {
            Grammar = grammar;
            InnerResolve();
            return this;
        }

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

        protected abstract void InnerResolve();
        protected abstract IMatcher MakeMatcher();
    }
}
