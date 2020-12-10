namespace SixComp
{
    public partial class ParseTree
    {
        public class SomeType : IType
        {
            public SomeType(IType type)
            {
                Type = type;
            }

            public IType Type { get; }

            public static SomeType Parse(Parser parser)
            {
                parser.Consume(ToKind.KwSome);
                var type = IType.Parse(parser);

                return new SomeType(type);
            }

            public override string ToString()
            {
                return $"some {Type}";
            }
        }
    }
}