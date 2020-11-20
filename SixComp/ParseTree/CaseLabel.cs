using SixComp.Support;
using System;

namespace SixComp.ParseTree
{
    public class CaseLabel
    {
        public static readonly TokenSet Firsts = new TokenSet(ToKind.KwCase, ToKind.KwDefault);

        public CaseLabel(CaseItemList caseItems)
        {
            CaseItems = caseItems;
        }

        public CaseItemList CaseItems { get; }

        public static CaseLabel Parse(Parser parser)
        {
            switch (parser.Current)
            {
                case ToKind.KwCase:
                    parser.ConsumeAny();
                    var items = CaseItemList.Parse(parser);
                    parser.Consume(ToKind.Colon);
                    return new CaseLabel(items);
                case ToKind.KwDefault:
                    parser.ConsumeAny();
                    parser.Consume(ToKind.Colon);
                    return new CaseLabel(new CaseItemList());
            }

            parser.Consume(Firsts);

            throw new NotSupportedException();
        }

        public override string ToString()
        {
            if (CaseItems.Count == 0)
            {
                return "default:";
            }
            return $"case {CaseItems}:";
        }
    }
}
