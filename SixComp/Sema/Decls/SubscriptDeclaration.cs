using SixComp.Entities;
using SixComp.Sema.Stmts;
using SixComp.Support;

namespace SixComp.Sema
{
    public class SubscriptDeclaration : BaseScoped<Tree.SubscriptDeclaration>, IDeclaration, IWithGenerics
    {
        public SubscriptDeclaration(IScoped outer, Tree.SubscriptDeclaration tree)
            : base(outer, tree)
        {
            Where = new GenericRestrictions(this);
            Generics = new GenericParameters(this, Tree.Generics);
            Parameters = new FuncParameters(this, Tree.Parameters);
            Result = ITypeDefinition.Build(Outer, Tree.Result);
            Where.Add(this, Tree.Requirements);
            Blocks = new PropertyBlocks(Outer, tree.Blocks);
        }

        public GenericParameters Generics { get; }
        public FuncParameters Parameters { get; }
        public ITypeDefinition Result { get; }
        public GenericRestrictions Where { get; }
        public PropertyBlocks Blocks { get; }

        public override void Report(IWriter writer)
        {
            using (writer.Indent(Strings.Head.Subscript))
            {
                Generics.Report(writer);
                Parameters.Report(writer);
                Result.Report(writer, Strings.Head.Result);
                Where.Report(writer);
                Blocks.Report(writer);
            }
        }
    }
}
