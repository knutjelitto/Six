using System;
using System.Collections.Generic;

namespace SixComp.Tree
{
    public class ArrayLiteral : ItemList<AnyExpression>, AnyPrimaryExpression
    {
        public ArrayLiteral(List<AnyExpression> items)
            : base(items)
        {
        }

        public AnyExpression? LastExpression => this;

        public static ArrayLiteral Parse(Parser parser)
        {
            parser.Consume(ToKind.LBracket);
            var items = new List<AnyExpression>();
            if (parser.Match(ToKind.RBracket))
            {
                return new ArrayLiteral(items); // Empty
            }
            do
            {
                if (parser.Current == ToKind.RBracket)
                {
                    break; // additional ','
                }
                var item = AnyExpression.TryParse(parser) ?? throw new InvalidOperationException($"{typeof(ArrayLiteral)}");
                items.Add(item);
            }
            while (parser.Match(ToKind.Comma));

            parser.Match(ToKind.RBracket);

            return new ArrayLiteral(items);
        }
    }
}
