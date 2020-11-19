using System;
using System.Collections.Generic;
using System.Text;

namespace SixComp.ParseTree
{
    public class TupleExpression : AnyPrimary
    {
        private TupleExpression(ExpressionList expressions)
        {
            Expressions = expressions;
        }

        public ExpressionList Expressions { get; }

        public static TupleExpression From(ExpressionList expressions)
        {
            return new TupleExpression(expressions);
        }
    }
}
