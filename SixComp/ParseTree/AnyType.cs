namespace SixComp.ParseTree
{
    public interface AnyType
    {
        public static AnyType Parse(Parser parser)
        {
            var type = TypeIdentifier.Parse(parser);
            switch (parser.Current.Kind)
            {
                case ToKind.Quest:
                    parser.ConsumeAny();
                    return new OptionalType(type);
            }

            return type;
        }
    }
}
