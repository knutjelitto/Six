using SixComp.Support;

namespace SixComp.Sema
{
    public class TypeExpression : Base<Tree.TypeExpression>, IExpression
    {
        public TypeExpression(IScoped outer, Tree.TypeExpression tree)
            : base(outer, tree)
        {
            Type = ITypeDefinition.Build(outer, tree.Type);
        }

        public ITypeDefinition Type { get; }

        public override void Report(IWriter writer)
        {
            Type.Report(writer, Strings.Head.Type);
        }
    }
}
