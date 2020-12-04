using SixComp.Support;

namespace SixComp.Sema
{
    public class FuncDeclaration : BaseScoped<Tree.FunctionDeclaration>, IDeclaration, IWhere, INamed
    {
        public FuncDeclaration(IScoped outer, Tree.FunctionDeclaration tree)
            : base(outer, tree)
        {
            Name = new BaseName(outer, tree.Name);
            Where = new GenericRestrictions(this);
            GenericParameters = new GenericParameters(this, Tree.GenericParameters);
            Parameters = new FuncParameters(this, Tree.Parameters);
            Result = IType.Build(this, Tree.Result);
            Where.Add(this, Tree.Requirements);
            Block = CodeBlock.Maybe(this, Tree.Block);
        }

        public BaseName Name { get; }
        public GenericParameters GenericParameters { get; }
        public FuncParameters Parameters { get; }
        public IType Result { get; }
        public GenericRestrictions Where { get; }
        public CodeBlock? Block { get; }

        public override void Report(IWriter writer)
        {
            Tree.Tree(writer);
            using (writer.Indent(Strings.Head.Func))
            {
                Name.Report(writer, Strings.Head.Name);
                GenericParameters.Report(writer);
                Parameters.Report(writer);
                Result.Report(writer, Strings.Head.Result);
                Where.Report(writer);
                Block?.Report(writer);
            }
        }
    }
}
