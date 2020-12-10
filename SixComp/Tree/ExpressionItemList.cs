using System.Collections.Generic;
using System.Linq;

namespace SixComp
{
    public partial class ParseTree
    {
        public class ExpressionItemList<T> : ItemList<T>, IExpression
        where T : IExpression
        {
            public ExpressionItemList(List<T> items) : base(items) { }
            public ExpressionItemList() { }

            public IExpression? LastExpression
            {
                get
                {
                    var last = this.LastOrDefault()?.LastExpression;
                    return last;
                }
            }
        }
    }
}