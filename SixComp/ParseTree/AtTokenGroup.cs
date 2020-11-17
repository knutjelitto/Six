namespace SixComp.ParseTree
{
    public class AtTokenGroup : AtToken
    {
        public AtTokenGroup(Token left, AtTokenList tokens, Token right)
        {
            Left = left;
            Tokens = tokens;
            Right = right;
        }

        public Token Left { get; }
        public AtTokenList Tokens { get; }
        public Token Right { get; }

        public static AtTokenGroup Parse(Parser parser, ToKind leftKind, ToKind rightKind)
        {
            var left = parser.Consume(leftKind);
            var list = parser.Current != rightKind
                ? AtTokenList.Parse(parser, rightKind)
                : new AtTokenList()
                ;
            var right = parser.Consume(rightKind);

            return new AtTokenGroup(left, list, right);
        }
    }
}
