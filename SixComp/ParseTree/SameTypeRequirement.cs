namespace SixComp.ParseTree
{
    public class SameTypeRequirement : AnyRequirement
    {
        private SameTypeRequirement(AnyType name, AnyType type)
        {
            Name = name;
            Type = type;
        }

        public AnyType Name { get; }
        public AnyType Type { get; }

        public static SameTypeRequirement From(AnyType name, AnyType type)
        {
            return new SameTypeRequirement(name, type);
        }
    }
}
