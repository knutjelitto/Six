namespace SixComp.Tree
{
    public class OptionalType : AnyType
    {
        public OptionalType(AnyType type)
        {
            Type = type;
        }

        public AnyType Type { get; }

        public override string ToString()
        {
            return $"{Type}?";
        }
    }
}
