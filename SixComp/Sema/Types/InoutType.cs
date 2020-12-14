using Six.Support;

namespace SixComp.Sema
{
    public class InoutType : Base<ParseTree.PrefixedType>, ITypeDefinition
    {
        public InoutType(IScoped outer, ParseTree.PrefixedType tree)
            : base(outer, tree)
        {
            Type = ITypeDefinition.Build(outer, tree.Type);
        }

        public ITypeDefinition Type { get; }

        public override void Report(IWriter writer)
        {
            Type.Report(writer, Strings.Head.InOut);
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
