using SixComp.Support;

namespace SixComp.Sema
{
    public class SubscriptDecl : Base<Tree.SubscriptDeclaration>, IDeclaration, IOwner
    {
        public SubscriptDecl(IScoped outer, Tree.SubscriptDeclaration declaration)
            : base(outer, declaration)
        {
            Where = new GenericRestrictions(this);
            GenericParameters = new GenericParameters(this, Tree.Generics);
            Parameters = new FuncParameters(this, Tree.Parameters);
            Result = IType.Build(Outer, Tree.Result);
            Where.Add(this, Tree.Requirements);
        }

        public GenericParameters GenericParameters { get; }
        public FuncParameters Parameters { get; }
        public IType Result { get; }
        public GenericRestrictions Where { get; }

        public override void Report(IWriter writer)
        {
            using (writer.Indent(Strings.Head.Subscript))
            {
                GenericParameters.Report(writer);
                Parameters.Report(writer);
                Result.Report(writer, Strings.Head.Result);
                Where.Report(writer);
                writer.WriteLine(Strings.Incomplete);
            }
        }
    }
}
