using System;
using System.Collections.Generic;

namespace SixComp
{
    public partial class ParseTree
    {
        public class ArrayLiteral : ItemList<IExpression>, IPrimaryExpression
        {
            public ArrayLiteral(List<IExpression> items)
                : base(items)
            {
            }

            public IExpression? LastExpression => this;

            public static ArrayLiteral Parse(Parser parser)
            {
                parser.Consume(ToKind.LBracket);
                var items = new List<IExpression>();
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
                    var item = IExpression.TryParse(parser) ?? throw new InvalidOperationException($"{typeof(ArrayLiteral)}");
                    items.Add(item);
                }
                while (parser.Match(ToKind.Comma));

                parser.Match(ToKind.RBracket);

                return new ArrayLiteral(items);
            }
        }
    }
}