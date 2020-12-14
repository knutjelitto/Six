using Six.Support;

namespace SixComp.Sema
{
    public class FunctionDeclaration : Nominal<ParseTree.FunctionDeclaration>
    {
        public FunctionDeclaration(IScoped outer, ParseTree.FunctionDeclaration tree)
            : base(outer, tree)
        {
            Parameters = new FuncParameters(this, Tree.Parameters);
            Result = ITypeDefinition.Build(this, Tree.Result);
            Block = CodeBlock.Maybe(this, Tree.Block);

            Declare(this);
        }

        public FuncParameters Parameters { get; }
        public ITypeDefinition Result { get; }
        public CodeBlock? Block { get; }

        public override void Report(IWriter writer)
        {
            Tree.Tree(writer);
            using (writer.Indent($"{Strings.Head.Func} {Name.Text}"))
            {
                Generics.Report(writer);
                Parameters.Report(writer);
                Result.Report(writer, Strings.Head.Result);
                Where.Report(writer);
                Block?.Report(writer);
            }
        }
    }
}
