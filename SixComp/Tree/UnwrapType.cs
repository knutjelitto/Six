namespace SixComp
{
    public partial class ParseTree
    {
        public class UnwrapType : IType
        {
            public UnwrapType(IType type)
            {
                Type = type;
            }

            public IType Type { get; }

            public override string ToString()
            {
                return $"{Type}!";
            }
        }
    }
}