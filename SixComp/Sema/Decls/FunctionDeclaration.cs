using SixComp.Entities;
using SixComp.Support;

namespace SixComp.Sema
{
    public class FunctionDeclaration : BaseScoped<Tree.FunctionDeclaration>, INamedDeclaration, IWithGenerics
    {
        public FunctionDeclaration(IScoped outer, Tree.FunctionDeclaration tree)
            : base(outer, tree)
        {
            Name = new BaseName(outer, tree.Name);
            Where = new GenericRestrictions(this);
            Generics = new GenericParameters(this, Tree.GenericParameters);
            Parameters = new FuncParameters(this, Tree.Parameters);
            Result = ITypeDefinition.Build(this, Tree.Result);
            Where.Add(this, Tree.Requirements);
            Block = CodeBlock.Maybe(this, Tree.Block);

            Declare(this);
        }

        public BaseName Name { get; }
        public GenericParameters Generics { get; }
        public FuncParameters Parameters { get; }
        public ITypeDefinition Result { get; }
        public GenericRestrictions Where { get; }
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
