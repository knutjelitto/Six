namespace SixComp
{
    public partial class ParseTree
    {
        public class OptionalType : IType
        {
            public OptionalType(IType type)
            {
                Type = type;
            }

            public IType Type { get; }

            public override string ToString()
            {
                return $"{Type}?";
            }
        }
    }
}