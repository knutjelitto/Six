namespace SixComp.Tree
{
    public class ConformanceRequirement : AnyRequirement
    {
        private ConformanceRequirement(AnyType name, ProtocolCompositionType composition)
        {
            Name = name;
            Composition = composition;
        }

        public AnyType Name { get; }
        public ProtocolCompositionType Composition { get; }

        public static ConformanceRequirement From(AnyType name, ProtocolCompositionType composition)
        {
            return new ConformanceRequirement(name, composition);
        }

        public override string ToString()
        {
            return $"{Name}: {Composition}";
        }
    }
}
