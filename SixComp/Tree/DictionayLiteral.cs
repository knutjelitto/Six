using System;
using System.Collections.Generic;

namespace SixComp.Tree
{
    public class DictionaryLiteral : ItemList<(AnyExpression, AnyExpression)>, AnyPrimaryExpression
    {
        public DictionaryLiteral(List<(AnyExpression, AnyExpression)> items)
            : base(items)
        {
        }

        public AnyExpression? LastExpression => this;

        public static DictionaryLiteral Parse(Parser parser)
        {
            parser.Consume(ToKind.LBracket);
            var items = new List<(AnyExpression, AnyExpression)>();
            if (parser.Current == ToKind.Colon)
            {
                parser.ConsumeAny();
                parser.Consume(ToKind.RBracket);
                return new DictionaryLiteral(items); // empty
            }
            do
            {
                if (parser.Current == ToKind.RBracket)
                {
                    break; // additional ','
                }
                var key = AnyExpression.TryParse(parser) ?? throw new InvalidOperationException($"{typeof(ArrayLiteral)}");
                parser.Consume(ToKind.Colon);
                var value = AnyExpression.TryParse(parser) ?? throw new InvalidOperationException($"{typeof(ArrayLiteral)}");
                items.Add((key, value));
            }
            while (parser.Match(ToKind.Comma));

            parser.Consume(ToKind.RBracket);

            return new DictionaryLiteral(items);
        }
    }
}
