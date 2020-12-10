using System.Collections.Generic;

namespace SixComp
{
    public partial class ParseTree
    {
        public class ExpressionList : ItemList<IExpression>
        {
            public ExpressionList(List<IExpression> items) : base(items) { }
            public ExpressionList() { }

            public override string ToString()
            {
                return string.Join(", ", this);
            }
        }
    }
}