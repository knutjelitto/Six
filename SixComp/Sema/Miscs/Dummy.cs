using SixComp.Support;

namespace SixComp.Sema
{
    public class Dummy : Base, IDeclaration, IExpression, IType, IStatement, IPattern
    {
        public Dummy(IScoped outer, object tree)
            : base(outer)
        {
            Tree = tree;

            Package.MissingTreeImplementations.Add(Tree.GetType().FullName!);
        }

        public object Tree { get; }

        public override void Report(IWriter writer)
        {
            writer.WriteLine($"{Strings.Missing} {Tree.GetType().Name}");
        }
    }
}
