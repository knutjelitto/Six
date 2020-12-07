using SixComp.Support;
using System.Collections.Generic;
using System.Linq;

namespace SixComp.Sema
{
    public class PatternLetVar<TTree> : Items<PatternInitializer, TTree>, IDeclaration
        where TTree: Tree.AnyDeclaration
    {
        protected PatternLetVar(IScoped outer, TTree tree, string reportLabel, Tree.PatternInitializerList patterns)
            : base(outer, tree, Enum(outer, patterns))
        {
            ReportLabel = reportLabel;
        }

        public string ReportLabel { get; }

        public override void Resolve(IWriter writer)
        {
            ResolveItems(writer);
        }

        public override void Report(IWriter writer)
        {
            Tree.Tree(writer);
            this.ReportList(writer, ReportLabel);
        }

        private static IEnumerable<PatternInitializer> Enum(IScoped outer, Tree.PatternInitializerList tree)
        {
            return tree.Select(initializer => new PatternInitializer(outer, initializer));
        }
    }

    public class PatternVar : PatternLetVar<Tree.PatternVarDeclaration>, IDeclaration
    {
        public PatternVar(IScoped outer, Tree.PatternVarDeclaration tree)
            : base(outer, tree, Strings.Head.Var, tree.Initializers)
        {
        }
    }

    public class PatternLet : PatternLetVar<Tree.LetDeclaration>, IDeclaration
    {
        public PatternLet(IScoped outer, Tree.LetDeclaration tree)
            : base(outer, tree, Strings.Head.Let, tree.Initializers)
        {
        }
    }
}
