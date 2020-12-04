using SixComp.Support;
using System.Collections.Generic;
using System.Linq;

namespace SixComp.Sema
{
    public interface IPattern: IReportable
    {
        public static IPattern? MaybeBuild(IScoped outer, Tree.AnyPattern? tree)
        {
            if (tree == null)
            {
                return null;
            }
            return Visit(outer, (dynamic)tree);
        }

        public static IPattern Build(IScoped outer, Tree.AnyPattern tree)
        {
            return Visit(outer, (dynamic)tree);
        }

        private static IPattern Visit(IScoped outer, Tree.TuplePattern tree)
        {
            return new TuplePattern(outer, tree);
        }

        private static IPattern Visit(IScoped outer, Tree.IdentifierPattern tree)
        {
            return new IdentifierPattern(outer, tree);
        }

        private static IPattern Visit(IScoped outer, Tree.LetPattern tree)
        {
            return new LetPattern(outer, tree);
        }

        private static IPattern Visit(IScoped outer, Tree.VarPattern tree)
        {
            return new VarPattern(outer, tree);
        }

        private static IPattern Visit(IScoped outer, Tree.AsPattern tree)
        {
            return new AsPattern(outer, tree);
        }

        private static IPattern Visit(IScoped outer, Tree.ExpressionPattern tree)
        {
            return new ExpressionPattern(outer, tree);
        }

        private static IPattern Visit(IScoped outer, Tree.OptionalPattern tree)
        {
            return new OptionalPattern(outer, tree);
        }

        private static IPattern Visit(IScoped outer, Tree.EnumCasePattern tree)
        {
            return new CasePattern(outer, tree);
        }

        private static IPattern Visit(IScoped outer, Tree.AnyPattern tree)
        {
            return new Dummy(outer, tree);
        }

        public class TuplePattern : Items<NamedPattern, Tree.TuplePattern>, IPattern
        {
            public TuplePattern(IScoped outer, Tree.TuplePattern tree)
                : base(outer, tree, Enum(outer, tree))
            {
            }

            public override void Report(IWriter writer)
            {
                this.ReportList(writer, Strings.Head.TuplePattern);
            }

            private static IEnumerable<NamedPattern> Enum(IScoped outer, Tree.TuplePattern tree)
            {
                return tree.Elements.Select(element => new NamedPattern(outer, element));
            }
        }

        public class IdentifierPattern : Base<Tree.IdentifierPattern>, IPattern
        {
            public IdentifierPattern(IScoped outer, Tree.IdentifierPattern tree)
                : base(outer, tree)
            {
                Name = new BaseName(outer, tree.Name);
            }

            public BaseName Name { get; }

            public override void Report(IWriter writer)
            {
                Name.Report(writer, Strings.Head.Identifier);
            }
        }

        public class LetPattern : Base<Tree.LetPattern>, IPattern
        {
            public LetPattern(IScoped outer, Tree.LetPattern tree)
                : base(outer, tree)
            {
                Pattern = Build(outer, tree.Pattern);
            }

            public IPattern Pattern { get; }

            public override void Report(IWriter writer)
            {
                Pattern.Report(writer, Strings.Head.Let);
            }
        }

        public class VarPattern : Base<Tree.VarPattern>, IPattern
        {
            public VarPattern(IScoped outer, Tree.VarPattern tree)
                : base(outer, tree)
            {
                Pattern = Build(outer, tree.Pattern);
            }

            public IPattern Pattern { get; }

            public override void Report(IWriter writer)
            {
                Pattern.Report(writer, Strings.Head.Var);
            }
        }

        public class AsPattern : Base<Tree.AsPattern>, IPattern
        {
            public AsPattern(IScoped outer, Tree.AsPattern tree)
                : base(outer, tree)
            {
                Pattern = Build(outer, tree.Pattern);
                Type = IType.Build(outer, tree.Type);
            }

            public IPattern Pattern { get; }
            public IType Type { get; }

            public override void Report(IWriter writer)
            {
                using (writer.Indent(Strings.Head.As))
                {
                    Pattern.Report(writer);
                    Type.Report(writer, Strings.Head.Type);
                }
            }
        }

        public class CasePattern : Base<Tree.EnumCasePattern>, IPattern
        {
            public CasePattern(IScoped outer, Tree.EnumCasePattern tree)
                : base(outer, tree)
            {
                EnumName = BaseName.Maybe(outer, tree.EnumName);
                CaseName = new BaseName(outer, tree.CaseName);
                Pattern = (TuplePattern?)MaybeBuild(outer, tree.TuplePattern);
            }

            public BaseName? EnumName { get; }
            public BaseName CaseName { get; }
            public TuplePattern? Pattern { get; }

            public override void Report(IWriter writer)
            {
                using (writer.Indent(Strings.Head.Case))
                {
                    EnumName?.Report(writer);
                    CaseName.Report(writer);
                    Pattern?.Report(writer);
                }
            }
        }

        public class ExpressionPattern : Base<Tree.ExpressionPattern>, IPattern
        {
            public ExpressionPattern(IScoped outer, Tree.ExpressionPattern tree)
                : base(outer, tree)
            {
                Expression = IExpression.Build(outer, tree.Expression);
            }

            public IExpression Expression { get; }
            public override void Report(IWriter writer)
            {
                Expression.Report(writer, Strings.Head.Expression);
            }
        }

        public class OptionalPattern : Base<Tree.OptionalPattern>, IPattern
        {
            public OptionalPattern(IScoped outer, Tree.OptionalPattern tree)
                : base(outer, tree)
            {
                Pattern = Build(outer, tree.Pattern);
            }

            public IPattern Pattern { get; }
            public override void Report(IWriter writer)
            {
                Pattern.Report(writer, Strings.Head.Optional);
            }
        }
    }
}
