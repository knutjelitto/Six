using SixComp.Support;

namespace SixComp.Sema
{
    public class NamedPattern : Base<Tree.TuplePatternElement>
    {
        public NamedPattern(IScoped outer, Tree.TuplePatternElement tree)
            : base(outer, tree)
        {
            Name = Tree.Name == null ? null : new BaseName(Outer, Tree.Name.Name);
            Pattern = IPattern.Build(Outer, Tree.Pattern);
        }

        public BaseName? Name { get; }
        public IPattern Pattern { get; }

        public override void Report(IWriter writer)
        {
            writer.WriteLine(Strings.NamedPattern);
            using (writer.Indent())
            {
                if (Name != null)
                {
                    Name.Report(writer);
                }
                Pattern.Report(writer);
            }
        }
    }
}
