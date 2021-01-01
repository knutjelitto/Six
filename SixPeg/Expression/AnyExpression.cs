﻿using SixPeg.Visiting;

namespace SixPeg.Expression
{
    public abstract class AnyExpression : IVisitableExpression
    {
        public Grammar Grammar { get; set; }
        public bool Spaced { get; set; } = false;

        public abstract T Accept<T>(IExpressionVisitor<T> visitor);
    }
}
