using Six.Support;
using System.Collections.Generic;
using System.Linq;

namespace SixComp.Sema
{
    public interface IPattern : IReportable
    {
        public static IPattern? MaybeBuild(IScoped outer, ParseTree.IPattern? tree)
        {
            return tree == null ? null : (IPattern)Visit(outer, (dynamic)tree);
        }

        public static IPattern Build(IScoped outer, ParseTree.IPattern tree)
        {
            return Visit(outer, (dynamic)tree);
        }

        private static IPattern Visit(IScoped outer, ParseTree.TuplePattern tree)
        {
            return new TuplePattern(outer, tree);
        }

        private static IPattern Visit(IScoped outer, ParseTree.IdentifierPattern tree)
        {
            return new IdentifierPattern(outer, tree);
        }

        private static IPattern Visit(IScoped outer, ParseTree.LetPattern tree)
        {
            return new LetPattern(outer, tree);
        }

        private static IPattern Visit(IScoped outer, ParseTree.VarPattern tree)
        {
            return new VarPattern(outer, tree);
        }

        private static IPattern Visit(IScoped outer, ParseTree.AsPattern tree)
        {
            return new AsPattern(outer, tree);
        }

        private static IPattern Visit(IScoped outer, ParseTree.ExpressionPattern tree)
        {
            return new ExpressionPattern(outer, tree);
        }

        private static IPattern Visit(IScoped outer, ParseTree.OptionalPattern tree)
        {
            return new OptionalPattern(outer, tree);
        }

        private static IPattern Visit(IScoped outer, ParseTree.EnumCasePattern tree)
        {
            return new CasePattern(outer, tree);
        }

        private static IPattern Visit(IScoped outer, ParseTree.IPattern tree)
        {
            return new Dummy(outer, tree);
        }

        public class TuplePattern : Items<NamedPattern, ParseTree.TuplePattern>, IPattern
        {
            public TuplePattern(IScoped outer, ParseTree.TuplePattern tree)
                : base(outer, tree, Enum(outer, tree))
            {
            }

            public override void Report(IWriter writer)
            {
                this.ReportList(writer, Strings.Head.TuplePattern);
            }

            private static IEnumerable<NamedPattern> Enum(IScoped outer, ParseTree.TuplePattern tree)
            {
                return tree.Elements.Select(element => new NamedPattern(outer, element));
            }
        }

        public class IdentifierPattern : Base<ParseTree.IdentifierPattern>, IPattern, INamedDeclaration
        {
            public IdentifierPattern(IScoped outer, ParseTree.IdentifierPattern tree)
                : base(outer, tree)
            {
                Name = new BaseName(outer, tree.Name);

                Declare(this);
            }

            public BaseName Name { get; }

            public override void Report(IWriter writer)
            {
                Name.Report(writer, Strings.Head.Identifier);
            }
        }

        public class LetPattern : Base<ParseTree.LetPattern>, IPattern
        {
            public LetPattern(IScoped outer, ParseTree.LetPattern tree)
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

        public class VarPattern : Base<ParseTree.VarPattern>, IPattern
        {
            public VarPattern(IScoped outer, ParseTree.VarPattern tree)
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

        public class AsPattern : Base<ParseTree.AsPattern>, IPattern
        {
            public AsPattern(IScoped outer, ParseTree.AsPattern tree)
                : base(outer, tree)
            {
                Pattern = Build(outer, tree.Pattern);
                Type = ITypeDefinition.Build(outer, tree.Type);
            }

            public IPattern Pattern { get; }
            public ITypeDefinition Type { get; }

            public override void Report(IWriter writer)
            {
                using (writer.Indent(Strings.Head.As))
                {
                    Pattern.Report(writer);
                    Type.Report(writer, Strings.Head.Type);
                }
            }
        }

        public class CasePattern : Base<ParseTree.EnumCasePattern>, IPattern
        {
            public CasePattern(IScoped outer, ParseTree.EnumCasePattern tree)
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

        public class ExpressionPattern : Base<ParseTree.ExpressionPattern>, IPattern
        {
            public ExpressionPattern(IScoped outer, ParseTree.ExpressionPattern tree)
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

        public class OptionalPattern : Base<ParseTree.OptionalPattern>, IPattern
        {
            public OptionalPattern(IScoped outer, ParseTree.OptionalPattern tree)
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

        public class NamedPattern : Base<ParseTree.TuplePatternElement>
        {
            public NamedPattern(IScoped outer, ParseTree.TuplePatternElement tree)
                : base(outer, tree)
            {
                Name = BaseName.Maybe(outer, tree.Name?.Name);
                Pattern = Build(Outer, Tree.Pattern);
            }

            public BaseName? Name { get; }
            public IPattern Pattern { get; }

            public override void Report(IWriter writer)
            {
                using (writer.Indent(Strings.Head.Named))
                {
                    Name.Report(writer, Strings.Head.Name);
                    Pattern.Report(writer);
                }
            }
        }
    }
}
