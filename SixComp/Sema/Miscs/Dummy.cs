using SixComp.Support;

namespace SixComp.Sema
{
    public class Dummy : Base, IDeclaration, IExpression, ITypeDefinition, IStatement, IPattern
    {
        public Dummy(IScoped outer, object tree)
            : base(outer)
        {
            Tree = tree;

            Module.Missings.Add(Tree.GetType().FullName!);
        }

        public object Tree { get; }

        public override void Report(IWriter writer)
        {
            writer.WriteLine($"{Strings.Missing} {Tree.GetType().Name}");
        }
    }
}
