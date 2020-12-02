using SixComp.Support;

namespace SixComp.Sema
{
    public class UnwrapType : Base<Tree.UnwrapType>, IType
    {
        public UnwrapType(IScoped outer, Tree.UnwrapType tree)
            : base(outer, tree)
        {
            Type = IType.Build(outer, tree.Type);
        }

        public IType Type { get; }

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
