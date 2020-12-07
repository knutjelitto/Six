using SixComp.Support;
using System.Collections.Generic;
using System.Linq;

namespace SixComp.Sema
{
    public class Switch : Base<Tree.SwitchStatement>, IStatement
    {
        public Switch(IScoped outer, Tree.SwitchStatement tree)
            : base(outer, tree)
        {
            Value = IExpression.Build(Outer, Tree.Value);
            Cases = new SwitchCases(Outer, Tree.Cases);
        }

        public IExpression Value { get; }
        public SwitchCases Cases { get; }

        public override void Resolve(IWriter writer)
        {
            Resolve(writer, Value, Cases);
        }

        public override void Report(IWriter writer)
        {
            using (writer.Indent(Strings.Head.Switch))
            {
                Value.Report(writer);
                Cases.Report(writer);
            }
        }

        public class SwitchCases : Items<SwitchCase, Tree.SwitchCaseClause>
        {
            public SwitchCases(IScoped outer, Tree.SwitchCaseClause tree)
                : base(outer, tree, Enum(outer, tree))
            {
            }

            public override void Resolve(IWriter writer)
            {
                ResolveItems(writer);
            }

            public override void Report(IWriter writer)
            {
                this.ReportList(writer, Strings.Head.Cases);
            }

            private static IEnumerable<SwitchCase> Enum(IScoped outer, Tree.SwitchCaseClause tree)
            {
                return tree.Select(c => new SwitchCase(outer, c));
            }
        }

        public class SwitchCase : Base<Tree.SwitchCase>
        {
            public SwitchCase(IScoped outer, Tree.SwitchCase tree)
                : base(outer, tree)
            {
                Label = new SwitchCaseLabel(this, tree.Label);
                Statements = new Statements(this, tree.Statements);
            }

            public SwitchCaseLabel Label { get; }
            public Statements Statements { get; }

            public override void Resolve(IWriter writer)
            {
                Resolve(writer, Label, Statements);
            }

            public override void Report(IWriter writer)
            {
                using (writer.Indent(Strings.Head.Case))
                {
                    Label.Report(writer);
                    Statements.Report(writer);
                }
            }
        }

        public class SwitchCaseLabel : Items<SwitchCaseItem, Tree.CaseLabel>
        {
            public SwitchCaseLabel(IScoped outer, Tree.CaseLabel tree)
                : base(outer, tree, Enum(outer, tree))
            {
            }

            public override void Resolve(IWriter writer)
            {
                ResolveItems(writer);
            }

            public override void Report(IWriter writer)
            {
                if (Count == 0)
                {
                    writer.WriteLine(Strings.Head.Default);
                }
                else
                {
                    this.ReportList(writer, Strings.Head.Items);
                }
            }

            private static IEnumerable<SwitchCaseItem> Enum(IScoped outer, Tree.CaseLabel tree)
            {
                return tree.CaseItems.Select(item => new SwitchCaseItem(outer, item));
            }
        }

        public class SwitchCaseItem : Base<Tree.CaseItem>
        {
            public SwitchCaseItem(IScoped outer, Tree.CaseItem tree)
                : base(outer, tree)
            {
                Pattern = IPattern.Build(outer, tree.Pattern);
                Where = IExpression.MaybeBuild(outer, tree.Where?.Expression);
            }

            public IPattern Pattern { get; }
            public IExpression? Where { get; }

            public override void Resolve(IWriter writer)
            {
                // TODO: Pattern??
                Resolve(writer, Where);
            }

            public override void Report(IWriter writer)
            {
                using (writer.Indent(Strings.Head.Item))
                {
                    Pattern.Report(writer);
                    Where?.Report(writer);
                }
            }
        }
    }
}
