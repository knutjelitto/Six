using SixComp.Support;

namespace SixComp.Sema
{
    public class OptionalType : Base<ParseTree.OptionalType>, ITypeDefinition
    {
        public OptionalType(IScoped outer, ParseTree.OptionalType tree)
            : base(outer, tree)
        {
            Type = ITypeDefinition.Build(Outer, Tree.Type);
        }

        public ITypeDefinition Type { get; }

        public override void Report(IWriter writer)
        {
            Type.Report(writer, Strings.Head.Optional);
        }
    }
}
