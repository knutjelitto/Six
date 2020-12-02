namespace SixComp.Tree
{
    public class UnwrapType : AnyType
    {
        public UnwrapType(AnyType type)
        {
            Type = type;
        }

        public AnyType Type { get; }

        public override string ToString()
        {
            return $"{Type}!";
        }
    }
}
