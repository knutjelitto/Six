using System;
using System.Collections.Generic;

namespace SixComp
{
    public partial class ParseTree
    {
        public class DictionaryLiteral : ItemList<(IExpression key, IExpression value)>, IPrimaryExpression
        {
            public DictionaryLiteral(List<(IExpression, IExpression)> items)
                : base(items)
            {
            }

            public IExpression? LastExpression => this;

            public static DictionaryLiteral Parse(Parser parser)
            {
                parser.Consume(ToKind.LBracket);
                var items = new List<(IExpression, IExpression)>();
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
                    var key = IExpression.TryParse(parser) ?? throw new InvalidOperationException($"{typeof(ArrayLiteral)}");
                    parser.Consume(ToKind.Colon);
                    var value = IExpression.TryParse(parser) ?? throw new InvalidOperationException($"{typeof(ArrayLiteral)}");
                    items.Add((key, value));
                }
                while (parser.Match(ToKind.Comma));

                parser.Consume(ToKind.RBracket);

                return new DictionaryLiteral(items);
            }
        }
    }
}