using SixComp.Support;

namespace SixComp.Sema
{
    public class FunctionDeclaration : BaseScoped<Tree.FunctionDeclaration>, INamedDeclaration, IWhere
    {
        public FunctionDeclaration(IScoped outer, Tree.FunctionDeclaration tree)
            : base(outer, tree)
        {
            Name = new BaseName(outer, tree.Name);
            Where = new GenericRestrictions(this);
            GenericParameters = new GenericParameters(this, Tree.GenericParameters);
            Parameters = new FuncParameters(this, Tree.Parameters);
            Result = ITypeDefinition.Build(this, Tree.Result);
            Where.Add(this, Tree.Requirements);
            Block = CodeBlock.Maybe(this, Tree.Block);

            Declare(this);
        }

        public BaseName Name { get; }
        public GenericParameters GenericParameters { get; }
        public FuncParameters Parameters { get; }
        public ITypeDefinition Result { get; }
        public GenericRestrictions Where { get; }
        public CodeBlock? Block { get; }

        public override void Resolve(IWriter writer)
        {
            Resolve(writer, GenericParameters, Parameters, Result, Where, Block);
        }

        public override void Report(IWriter writer)
        {
            Tree.Tree(writer);
            using (writer.Indent($"{Strings.Head.Func} {Name.Text}"))
            {
                GenericParameters.Report(writer);
                Parameters.Report(writer);
                Result.Report(writer, Strings.Head.Result);
                Where.Report(writer);
                Block?.Report(writer);
            }
        }
    }
}
