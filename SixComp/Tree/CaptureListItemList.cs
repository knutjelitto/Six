using System.Collections.Generic;

namespace SixComp
{
    public partial class ParseTree
    {
        public class CaptureListItemList : ItemList<CaptureListItem>
        {
            public CaptureListItemList(List<CaptureListItem> captures) : base(captures) { }
            public CaptureListItemList() { }

            public static CaptureListItemList Parse(Parser parser)
            {
                var captures = new List<CaptureListItem>();

                do
                {
                    var capture = CaptureListItem.Parse(parser);
                    captures.Add(capture);
                }
                while (parser.Match(ToKind.Comma));

                return new CaptureListItemList(captures);
            }
        }
    }
}