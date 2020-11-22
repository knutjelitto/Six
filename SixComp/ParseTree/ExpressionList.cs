using System.Collections.Generic;

namespace SixComp.ParseTree
{
    public class ExpressionList : ItemList<AnyExpression>
    {
        public ExpressionList(List<AnyExpression> items) : base(items) { }
        public ExpressionList() { }

        public override string ToString()
        {
            return string.Join(", ", this);
        }
    }
}
