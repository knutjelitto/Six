﻿using System.Collections.Generic;

namespace SixComp
{
    public partial class ParseTree
    {
        public class EnumCaseItemList : ItemList<EnumCaseItem>
        {
            public EnumCaseItemList(List<EnumCaseItem> items) : base(items) { }
            public EnumCaseItemList() { }

            public static EnumCaseItemList Parse(Parser parser)
            {
                var caseItems = new List<EnumCaseItem>();

                do
                {
                    var caseItem = EnumCaseItem.Parse(parser);
                    caseItems.Add(caseItem);
                }
                while (parser.Match(ToKind.Comma));

                return new EnumCaseItemList(caseItems);
            }

            public override string ToString()
            {
                return string.Join(", ", this);
            }
        }
    }
}