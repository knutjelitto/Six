namespace SixComp.ParseTree
{
    public class WillSetBlock : AnyPropertyBlock
    {
        private WillSetBlock(Prefix prefix, Token keyword, SetterName? setterName, CodeBlock block)
            : base(prefix, keyword, setterName, block)
        {
        }

        public static WillSetBlock Parse(Parser parser, Prefix prefix)
        {
            var willset = parser.Consume(ToKind.KwWillSet);
            var setterName = parser.Try(SetterName.Firsts, SetterName.Parse);
            var block = CodeBlock.Parse(parser);

            return new WillSetBlock(prefix, willset, setterName, block);
        }
    }
}
