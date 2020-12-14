using Six.Support;

namespace SixComp.Sema
{
    public class UnwrapType : Base<ParseTree.UnwrapType>, ITypeDefinition
    {
        public UnwrapType(IScoped outer, ParseTree.UnwrapType tree)
            : base(outer, tree)
        {
            Type = ITypeDefinition.Build(outer, tree.Type);
        }

        public ITypeDefinition Type { get; }

        public override void Report(IWriter writer)
        {
            Type.Report(writer, Strings.Head.Unwrap);
        }

        public override string ToString()
        {
            var type = Type.ToString()!;
            if (!type.StartsWith("SixComp."))
            {
                return $"{Strings.KwInOut} {type}";
            }

            return base.ToString()!;
        }
    }
}
