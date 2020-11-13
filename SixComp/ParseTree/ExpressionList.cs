using System.Collections.Generic;

namespace SixComp.ParseTree
{
    public class ExpressionList : ItemList<Expression>
    {
        public ExpressionList(List<Expression> items) : base(items) { }
        public ExpressionList() { }
    }
}
