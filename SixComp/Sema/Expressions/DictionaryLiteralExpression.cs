using SixComp.Support;
using System.Collections.Generic;
using System.Linq;

namespace SixComp.Sema
{
    public class DictionaryLiteralExpression : Items<KeyValue, Tree.DictionaryLiteral>, IExpression
    {
        public DictionaryLiteralExpression(IScoped outer, Tree.DictionaryLiteral tree)
            : base(outer, tree, Enum(outer, tree))
        {
        }

        public override void Report(IWriter writer)
        {
            this.ReportList(writer, Strings.Head.DictionaryLit);
        }

        private static IEnumerable<KeyValue> Enum(IScoped outer, Tree.DictionaryLiteral tree)
        {
            return tree.Select(expression => new KeyValue(outer, IExpression.Build(outer, expression.key), IExpression.Build(outer, expression.value)));
        }
    }
}
