using SixComp.Support;

namespace SixComp.ParseTree
{
    public class CaptureList
    {
        public static readonly TokenSet Firsts = new TokenSet(ToKind.LBracket);

        public CaptureList(CaptureListItemList captures)
        {
            Captures = captures;
        }

        public CaptureListItemList Captures { get; }

        public static CaptureList Parse(Parser parser)
        {
            parser.Consume(ToKind.LBracket);
            var captures = CaptureListItemList.Parse(parser);
            parser.Consume(ToKind.RBracket);

            return new CaptureList(captures);
        }
    }
}
