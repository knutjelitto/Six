using SixComp.Support;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SixComp.Sema
{
    public class Closure : Base<Tree.ClosureExpression>, IExpression
    {
        public Closure(IScoped outer, Tree.ClosureExpression tree)
            : base(outer, tree)
        {
            if (tree.Captures.Captures.Count > 0)
            {
                throw new NotImplementedException();
            }
            Parameters = new ClosureParameters(outer, tree.Parameters);
            Throws = tree.Throws;
            Result = IType.MaybeBuild(outer, tree.Result);
            Statements = new Statements(outer, tree.Statements);

        }

        public ClosureParameters Parameters { get; }
        public bool Throws { get; }
        public IType? Result { get; }
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

        public class ClosureParameters : Items<TypedName, Tree.ClosureParameterClause>
        {
            public ClosureParameters(IScoped outer, Tree.ClosureParameterClause tree)
                : base(outer, tree, Enum(outer, tree))
            {
            }

            public override void Report(IWriter writer)
            {
                this.ReportList(writer, Strings.Head.Parameters);
            }

            private static IEnumerable<TypedName> Enum(IScoped outer, Tree.ClosureParameterClause tree)
            {
                return tree.Parameters.Select(parameter => new TypedName(outer, parameter.Name, parameter.Type));
            }
        }
    }
}
