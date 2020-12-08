using SixComp.Support;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SixComp.Sema
{
    public class ClosureExpression : BaseScoped<Tree.ClosureExpression>, IExpression
    {
        public ClosureExpression(IScoped outer, Tree.ClosureExpression tree)
            : base(outer, tree)
        {
            if (tree.Captures.Captures.Count > 0)
            {
                throw new NotImplementedException();
            }
            Parameters = new ClosureParameters(this, tree.Parameters);
            Throws = tree.Throws;
            Result = ITypeDefinition.MaybeBuild(this, tree.Result);
            Statements = new Statements(this, tree.Statements);

        }

        public ClosureParameters Parameters { get; }
        public bool Throws { get; }
        public ITypeDefinition? Result { get; }
        public Statements Statements { get; }

        public override void Report(IWriter writer)
        {
            using (writer.Indent(Strings.Head.Closure))
            {
                Parameters.Report(writer);
                Throws.Report(writer, Strings.Head.Throws);
                Result.Report(writer, Strings.Head.Result);
                Statements.Report(writer);
            }
        }

        public class ClosureParameters : Items<ClosureParameter, Tree.ClosureParameterClause>
        {
            public ClosureParameters(IScoped outer, Tree.ClosureParameterClause tree)
                : base(outer, tree, Enum(outer, tree))
            {
            }

            public override void Report(IWriter writer)
            {
                this.ReportList(writer, Strings.Head.Parameters);
            }

            public void AddImplicit(BaseName name)
            {
                Add(new ClosureParameter(Outer, (Tree.BaseName)name.Tree, null));
            }

            private static IEnumerable<ClosureParameter> Enum(IScoped outer, Tree.ClosureParameterClause tree)
            {
                return tree.Parameters.Select(parameter => new ClosureParameter(outer, parameter.Name, parameter.Type));
            }
        }

        public class ClosureParameter : Base<Tree.BaseName>, INamedDeclaration
        {
            public ClosureParameter(IScoped outer, Tree.BaseName tree, Tree.AnyType? treeType)
                : base(outer, tree)
            {
                Name = new BaseName(outer, tree);
                Type = ITypeDefinition.MaybeBuild(outer, treeType);
                TreeType = treeType;

                Declare(this);
            }

            public BaseName Name { get; }
            public ITypeDefinition? Type { get; }
            public Tree.AnyType? TreeType { get; }

            public override void Report(IWriter writer)
            {
                using (writer.Indent(Strings.Head.TypedName))
                {
                    Name.Report(writer, Strings.Head.Name);
                    Type.Report(writer, Strings.Head.Type);
                }
            }
        }

    }
}
