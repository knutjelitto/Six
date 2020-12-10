using SixComp.Support;

namespace SixComp.Sema
{
    public class PatternInitializer : Base<ParseTree.PatternInitializer>
    {
        public PatternInitializer(IScoped outer, ParseTree.PatternInitializer tree)
            : base(outer, tree)
        {
            Pattern = IPattern.Build(outer, tree.Pattern);
            Type = ITypeDefinition.MaybeBuild(outer, tree.Type);
            Initializer = IExpression.MaybeBuild(outer, tree.Initializer?.Expression);
        }

        public IPattern Pattern { get; }
        public ITypeDefinition? Type { get; }
        public IExpression? Initializer { get; }

        public override void Report(IWriter writer)
        {
            using (writer.Indent(Strings.Head.Pattern))
            {
                Pattern.Report(writer);
                Type.Report(writer, Strings.Head.Type);
                Initializer.Report(writer, Strings.Head.Initializer);
            }
        }
    }
}
