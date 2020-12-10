using SixComp.Support;
using System.Collections.Generic;
using System.Linq;

namespace SixComp.Sema
{
    public class TupleExpression : Items<TupleExpression.TupleElement, ParseTree.TupleExpression>, IExpression
    {
        public TupleExpression(IScoped outer, ParseTree.TupleExpression tree)
            : base(outer, tree, Enum(outer, tree))
        {
        }

        public override void Report(IWriter writer)
        {
            this.ReportList(writer, Strings.Head.TupleExpression);
        }

        private static IEnumerable<TupleElement> Enum(IScoped outer, ParseTree.TupleExpression tree)
        {
            return tree.Elements.Select(element => new TupleElement(outer, element));
        }

        public class TupleElement : Base<ParseTree.TupleElement>
        {
            public TupleElement(IScoped outer, ParseTree.TupleElement tree)
                : base(outer, tree)
            {
                Name = Tree.Name == null ? null : new BaseName(Outer, Tree.Name);
                Value = IExpression.Build(Outer, Tree.Value);
            }

            public BaseName? Name { get; }
            public IExpression Value { get; }

            public override void Report(IWriter writer)
            {
                using (writer.Indent(Strings.Head.NamedValue))
                {
                    Name.Report(writer, Strings.Head.Name);
                    Value.Report(writer, Strings.Head.Value);
                }
            }
        }

    }
}
