using SixComp.Support;

namespace SixComp.Sema
{
    public class NamedValue : Base<Tree.TupleElement>
    {
        public NamedValue(IScoped outer, Tree.TupleElement tree)
            : base(outer, tree)
        {
            Name = Tree.Name == null ? null : new BaseName(Outer, Tree.Name);
            Value = IExpression.Build(Outer, Tree.Value);
        }

        public BaseName? Name { get; }
        public IExpression Value { get; }

        public override void Report(IWriter writer)
        {
            writer.WriteLine(Strings.NamedValue);
            using (writer.Indent())
            {
                if (Name != null)
                {
                    Name.Report(writer);
                }
                Value.Report(writer);
            }
        }
    }
}
