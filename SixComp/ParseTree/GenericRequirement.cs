namespace SixComp.ParseTree
{
    public class GenericRequirement
    {
        public GenericRequirement(ProtocolCompositionType composition)
        {
            Composition = composition;
        }

        public ProtocolCompositionType Composition { get; }

        public static GenericRequirement Parse(Parser parser)
        {
            parser.Consume(ToKind.Colon);

            var composition = ProtocolCompositionType.Parse(parser);

            return new GenericRequirement(composition);
        }
    }
}
