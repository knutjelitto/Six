using SixComp.Support;
using System;

namespace SixComp
{
    public partial class ParseTree
    {
        public class CaseLabel
        {
            public static readonly TokenSet Firsts = new TokenSet(ToKind.KwCase, ToKind.KwDefault);

            public CaseLabel(Prefix prefix, CaseItemList caseItems)
            {
                Prefix = prefix;
                CaseItems = caseItems;
            }

            public Prefix Prefix { get; }
            public CaseItemList CaseItems { get; }

            public static CaseLabel Parse(Parser parser)
            {
                var prefix = Prefix.Parse(parser, onlyAttributes: true);
                switch (parser.Current)
                {
                    case ToKind.KwCase:
                        parser.ConsumeAny();
                        var items = CaseItemList.Parse(parser);
                        parser.Consume(ToKind.Colon);
                        return new CaseLabel(prefix, items);
                    case ToKind.KwDefault:
                        parser.ConsumeAny();
                        parser.Consume(ToKind.Colon);
                        return new CaseLabel(prefix, new CaseItemList());
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
}