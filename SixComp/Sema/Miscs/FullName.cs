using SixComp.Support;

namespace SixComp.Sema
{
    public class FullName : Base<Tree.FullName>, IType, IExpression
    {
        public FullName(IScoped outer, Tree.FullName tree)
            : base(outer, tree)
        {
            Name = new BaseName(Outer, Tree.Name);
            Arguments = new GenericArguments(Outer, Tree.Arguments);
        }

        public BaseName Name { get; }
        public GenericArguments Arguments { get; }

        public bool IsSimplest => Arguments.Count == 0;

        public override void Report(IWriter writer)
        {
            writer.WriteLine($"{Name.Text}");
            Arguments.Report(writer);
        }

        public override string ToString()
        {
            if (IsSimplest)
            {
                return Name.Text;
            }
            return base.ToString()!;
        }
    }
}
