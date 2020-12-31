﻿using Pegasus.Common;
using SixPeg.Matchers;
using SixPeg.Visiting;
using System.Diagnostics;

namespace SixPeg.Expression
{
    public abstract class AnyExpression : IVisitableExpression
    {
        private AnyMatcher matcher;

        public Grammar Grammar { get; set; }
        public bool Spaced { get; set; } = false;

        [DebuggerStepThrough]
        public AnyMatcher GetMatcher()
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

        protected abstract AnyMatcher MakeMatcher();

        public abstract T Accept<T>(IExpressionVisitor<T> visitor);
    }
}
