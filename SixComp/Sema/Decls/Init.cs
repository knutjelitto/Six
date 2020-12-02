using SixComp.Support;
using System;

namespace SixComp.Sema
{
    public class Init : Base<Tree.InitializerDeclaration>, IDeclaration, IOwner
    {
        public enum InitKind
        {
            Init,
            InitChain,
            InitForce,
        }

        public Init(IScoped outer, Tree.InitializerDeclaration tree)
            : base(outer, tree)
        {
            Kind = tree.Kind switch
            {
                SixComp.Tree.InitializerDeclaration.InitKind.Init => InitKind.Init,
                SixComp.Tree.InitializerDeclaration.InitKind.InitChain => InitKind.InitChain,
                SixComp.Tree.InitializerDeclaration.InitKind.InitForce => InitKind.InitForce,
                _ => throw new ArgumentOutOfRangeException(),
            };
            Where = new GenericRestrictions(this);
            GenericParameters = new GenericParameters(this, Tree.GenericParameters);
            Parameters = new FuncParameters(this, Tree.Parameters);
            Where.Add(this, Tree.Requirements);
            Block = (Block?)IStatement.MaybeBuild(this, Tree.Block);
        }

        public InitKind Kind { get; }
        public GenericParameters GenericParameters { get; }
        public FuncParameters Parameters { get; }
        public GenericRestrictions Where { get; }
        public Block? Block { get; }

        public override void Report(IWriter writer)
        {
            Tree.Tree(writer);
            using (writer.Indent(Strings.Head.Init))
            {
                Kind.Report(writer, Strings.Head.Kind);
                GenericParameters.Report(writer);
                Parameters.Report(writer);
                Where.Report(writer);
                Block?.Report(writer);
            }
        }
    }
}
