namespace SixComp.ParseTree
{
    public class DidSetBlock : AnyPropertyBlock
    {
        private DidSetBlock(Prefix prefix, Token keyword, SetterName? setterName, CodeBlock block)
            : base(prefix, keyword, setterName, block)
        {
        }

        public static DidSetBlock Parse(Parser parser, Prefix prefix)
        {
            var didSet = parser.Consume(ToKind.KwDidSet);

            var setterName = parser.Try(SetterName.Firsts, SetterName.Parse);
            var block = CodeBlock.Parse(parser);

            return new DidSetBlock(prefix, didSet, setterName, block);
        }
    }
}
