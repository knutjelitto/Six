namespace SixComp.ParseTree
{
    public class GetBlock : AnyPropertyBlock
    {
        public GetBlock(Prefix prefix, Token keyword, CodeBlock? block)
            : base(prefix, keyword, null, block)
        {
        }

        public static GetBlock Parse(Parser parser, Prefix prefix)
        {
            var get = parser.Consume(ToKind.KwGet);

            var block = parser.Current == ToKind.LBrace
                ? CodeBlock.Parse(parser)
                : null;

            return new GetBlock(prefix, get, block);
        }
    }
}
