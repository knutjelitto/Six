using SixComp.Support;

namespace SixComp.Sema
{
    public class NameExpression : Expression<Tree.NameExpression>, INamed
    {
        public NameExpression(IScoped outer, Tree.NameExpression tree)
            : base(outer, tree)
        {
            Name = new BaseName(Outer, Tree.Name);
            Arguments = new GenericArguments(Outer, Tree.Arguments);
        }
        public BaseName Name { get; }
        public GenericArguments Arguments { get; }

        public override void Report(IWriter writer)
        {
            using (writer.Indent(Strings.Head.Name))
            {
                writer.WriteLine($"{Name}");
                Arguments.Report(writer);
            }
        }
    }
}
