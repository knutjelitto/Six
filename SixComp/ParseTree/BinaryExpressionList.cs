using System.Collections.Generic;

namespace SixComp.ParseTree
{
    public class BinaryExpressionList : ItemList<BinaryExpression>
    {
        public BinaryExpressionList(List<BinaryExpression> binaries) : base(binaries) { }
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
    }
}
