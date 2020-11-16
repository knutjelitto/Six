using System;
using System.Collections.Generic;
using System.Text;

namespace SixComp.ParseTree
{
    public class ConditionalExpression : AnyExpression
    {
        public ConditionalExpression(AnyExpression condition, AnyExpression ifTrue, AnyExpression ifFalse)
        {
            Condition = condition;
            IfTrue = ifTrue;
            IfFalse = ifFalse;
        }

        public AnyExpression Condition { get; }
        public AnyExpression IfTrue { get; }
        public AnyExpression IfFalse { get; }

        public override string ToString()
        {
            return $"(cond {Condition} {IfTrue} {IfFalse})";
        }
    }
}
