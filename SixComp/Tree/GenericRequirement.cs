namespace SixComp
{
    public partial class ParseTree
    {
        public class GenericRequirement : ITypeRestriction
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

            public override string ToString()
            {
                return $": {Composition}";
            }
        }
    }
}