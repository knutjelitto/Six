using System.Collections.Generic;

namespace SixComp.Tree
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
