using System;

namespace SixComp.ParseTree
{
    public class CaseLabel
    {
        public CaseLabel(CaseItemList caseItems)
        {
            CaseItems = caseItems;
        }

        public CaseItemList CaseItems { get; }

        public static CaseLabel Parse(Parser parser)
        {
            switch (parser.Current.Kind)
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

            throw new InvalidOperationException();
        }
    }
}
