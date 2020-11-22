using System.Collections.Generic;
using System.Linq;

namespace SixComp.ParseTree
{
    public class ExpressionItemList<T> : ItemList<T>, AnyExpression
        where T : AnyExpression
    {
        public ExpressionItemList(List<T> items) : base(items) { }
        public ExpressionItemList() { }

        public AnyExpression? LastExpression
        {
            get
            {
                var last = this.LastOrDefault()?.LastExpression;
                return last;
            }
        }
    }
}
