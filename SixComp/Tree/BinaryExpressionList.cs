using System.Collections.Generic;

namespace SixComp
{
    public partial class Tree
    {
        public class BinaryExpressionList : ItemList<BinaryExpression>
        {
            private BinaryExpressionList(List<BinaryExpression> binaries) : base(binaries) { }
            public BinaryExpressionList() { }

            public static BinaryExpressionList Parse(Parser parser)
            {
                var binaries = new List<BinaryExpression>();
                BinaryExpression? binary;

                while ((binary = BinaryExpression.TryParse(parser)) != null)
                {
                    binaries.Add(binary);
                }

                return new BinaryExpressionList(binaries);
            }

            public override string ToString()
            {
                if (Count > 0)
                {
                    return " " + string.Join(" ", this);
                }
                return string.Empty;
            }
        }
    }
}