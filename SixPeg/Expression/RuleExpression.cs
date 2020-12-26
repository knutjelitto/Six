﻿namespace SixPeg.Expression
{
    public class RuleExpression : AnyRule
    {
        public RuleExpression(Symbol name, AnyExpression expression)
            : base(name, expression, false)
        {
        }

        public override T Accept<T>(IVisitor<T> visitor)
        {
            return visitor.Visit(this);
        }
    }
}
