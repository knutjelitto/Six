using SixComp.Support;
using System.Collections.Generic;

namespace SixComp.ParseTree
{
    public class TrailingClosureList : ItemList<TrailingClosure>
    {
        public static readonly TokenSet Firsts = new TokenSet(ToKind.LBrace);

        public TrailingClosureList(List<TrailingClosure> closures) : base(closures) { }
        public TrailingClosureList() { }

        public static TrailingClosureList Parse(Parser parser)
        {
            var closures = new List<TrailingClosure>();
            var first = true;

            do
            {
                var closure = TrailingClosure.Parse(parser, first);
                closures.Add(closure);
                first = false;
            }
            while (parser.Current == ToKind.Name && parser.Next == ToKind.Colon && parser.Next == ToKind.LBrace);

            return new TrailingClosureList(closures);
        }

        public override string ToString()
        {
            return Missing ? string.Empty : " " + string.Join(" ", this);
        }
    }
}
