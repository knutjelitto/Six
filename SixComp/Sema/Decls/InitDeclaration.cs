using SixComp.Common;
using SixComp.Support;

namespace SixComp.Sema
{
    public class InitDeclaration : Base<Tree.InitializerDeclaration>, IDeclaration, IWhere
    {
        public InitDeclaration(IScoped outer, Tree.InitializerDeclaration tree)
            : base(outer, tree)
        {
            Kind = tree.Kind;
            Where = new GenericRestrictions(this);
            GenericParameters = new GenericParameters(this, Tree.GenericParameters);
            Parameters = new FuncParameters(this, Tree.Parameters);
            Where.Add(this, Tree.Requirements);
            Block = (CodeBlock?)IStatement.MaybeBuild(this, Tree.Block);
        }

        public InitKind Kind { get; }
        public GenericParameters GenericParameters { get; }
        public FuncParameters Parameters { get; }
        public GenericRestrictions Where { get; }
        public CodeBlock? Block { get; }

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
