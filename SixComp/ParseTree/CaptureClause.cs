using SixComp.Support;

namespace SixComp.ParseTree
{
    public class CaptureClause
    {
        public static readonly TokenSet Firsts = new TokenSet(ToKind.LBracket);

        public CaptureClause(CaptureListItemList captures)
        {
            Captures = captures;
        }

        public CaptureClause()
            : this(new CaptureListItemList())
        {
        }

        public bool Missing => Captures.Missing;

        public CaptureListItemList Captures { get; }

        public static CaptureClause Parse(Parser parser)
        {
            parser.Consume(ToKind.LBracket);
            var captures = CaptureListItemList.Parse(parser);
            parser.Consume(ToKind.RBracket);

            return new CaptureClause(captures);
        }

        public override string ToString()
        {
            if (Missing)
            {
                return string.Empty;
            }
            return $"[{Captures}]";
        }
    }
}
