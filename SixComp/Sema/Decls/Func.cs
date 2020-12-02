using SixComp.Support;

namespace SixComp.Sema
{
    public class Func : Base<Tree.FunctionDeclaration>, IDeclaration, IOwner, INamed
    {
        public Func(IScoped outer, Tree.FunctionDeclaration tree)
            : base(outer, tree)
        {
            Name = new BaseName(outer, tree.Name);
            Where = new GenericRestrictions(this);
            GenericParameters = new GenericParameters(this, Tree.GenericParameters);
            Parameters = new FuncParameters(this, Tree.Parameters);
            Result = IType.Build(Outer, Tree.Result);
            Where.Add(this, Tree.Requirements);
            Block = IStatement.MaybeBuild(this, Tree.Block);
        }

        public BaseName Name { get; }
        public GenericParameters GenericParameters { get; }
        public FuncParameters Parameters { get; }
        public IType Result { get; }
        public GenericRestrictions Where { get; }
        public IStatement? Block { get; }

        public override void Report(IWriter writer)
        {
            Tree.Tree(writer);
            using (writer.Indent(Strings.Head.Func))
            {
                Name.Report(writer);
                GenericParameters.Report(writer);
                Parameters.Report(writer);
                Result.Report(writer, Strings.Head.Result);
                Where.Report(writer);
                Block?.Report(writer);
            }
        }
    }
}
