using SixComp.Support;

namespace SixComp.Sema
{
    public class FuncTypeParameter : Base
    {
        public FuncTypeParameter(IScoped outer, Tree.FunctionTypeArgument tree)
            : base(outer)
        {
            Tree = tree;

            Extern = Tree.Extern?.ToString();
            Type = IType.Build(Outer, Tree.Type);
        }

        public Tree.FunctionTypeArgument Tree { get; }

        public string? Extern { get; }
        public IType Type { get; }

        public override void Report(IWriter writer)
        {
            writer.Write($"{Extern.SpEnd()}");
            Type.Report(writer);
        }
    }
}
