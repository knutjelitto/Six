namespace SixComp.ParseTree
{
    public class SetBlock : AnyPropertyBlock
    {
        public SetBlock(Prefix prefix, Token keyword, SetterName? setterName, CodeBlock? block)
            : base(prefix, keyword, setterName, block)
        {
        }

        public static SetBlock Parse(Parser parser, Prefix prefix)
        {
            var set = parser.Consume(ToKind.KwSet);

            var name = parser.Try(SetterName.Firsts, SetterName.Parse);

            var block = parser.Current == ToKind.LBrace
                ? CodeBlock.Parse(parser)
                : null;

            return new SetBlock(prefix, set, name, block);
        }
    }
}
