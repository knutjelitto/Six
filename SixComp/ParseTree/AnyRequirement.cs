namespace SixComp.ParseTree
{
    public interface AnyRequirement
    {
        public static AnyRequirement Parse(Parser parser)
        {
            var typeIdentifier = TypeIdentifier.Parse(parser);

            if (parser.Match(ToKind.EqualEqual))
            {
                var type = AnyType.Parse(parser);

                return SameTypeRequirement.From(typeIdentifier, type);
            }

            parser.Match(ToKind.Colon);

            var composition = ProtocolCompositionType.Parse(parser);

            return ConformanceRequirement.From(typeIdentifier, composition);
        }
    }
}
