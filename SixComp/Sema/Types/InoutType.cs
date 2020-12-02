using SixComp.Support;

namespace SixComp.Sema.Types
{
    public class InoutType : Base<Tree.PrefixedType>, IType
    {
        public InoutType(IScoped outer, Tree.PrefixedType tree)
            : base(outer, tree)
        {
            Type = IType.Build(outer, tree.Type);
        }

        public IType Type { get; }

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
