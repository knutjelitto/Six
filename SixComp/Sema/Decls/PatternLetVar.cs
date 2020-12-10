using SixComp.Support;
using System.Collections.Generic;
using System.Linq;

namespace SixComp.Sema
{
    public class PatternLetVar<TTree> : Items<PatternInitializer, TTree>, IDeclaration
        where TTree: ParseTree.IDeclaration
    {
        protected PatternLetVar(IScoped outer, TTree tree, string reportLabel, ParseTree.PatternInitializerList patterns)
            : base(outer, tree, Enum(outer, patterns))
        {
            ReportLabel = reportLabel;
        }

        public string ReportLabel { get; }

        public override void Report(IWriter writer)
        {
            Tree.Tree(writer);
            this.ReportList(writer, ReportLabel);
        }

        private static IEnumerable<PatternInitializer> Enum(IScoped outer, ParseTree.PatternInitializerList tree)
        {
            return tree.Select(initializer => new PatternInitializer(outer, initializer));
        }
    }

    public class PatternVar : PatternLetVar<ParseTree.PatternVarDeclaration>, IDeclaration
    {
        public PatternVar(IScoped outer, ParseTree.PatternVarDeclaration tree)
            : base(outer, tree, Strings.Head.Var, tree.Initializers)
        {
        }
    }

    public class PatternLet : PatternLetVar<ParseTree.LetDeclaration>, IDeclaration
    {
        public PatternLet(IScoped outer, ParseTree.LetDeclaration tree)
            : base(outer, tree, Strings.Head.Let, tree.Initializers)
        {
        }
    }
}
