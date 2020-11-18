namespace SixComp.ParseTree
{
    public class ConformanceRequirement : AnyRequirement
    {
        private ConformanceRequirement(TypeIdentifier typeIdentifier, ProtocolCompositionType composition)
        {
            TypeIdentifier = typeIdentifier;
            Composition = composition;
        }

        public TypeIdentifier TypeIdentifier { get; }
        public ProtocolCompositionType Composition { get; }

        public static ConformanceRequirement From(TypeIdentifier typeIdentifier, ProtocolCompositionType composition)
        {
            return new ConformanceRequirement(typeIdentifier, composition);
        }
    }
}
