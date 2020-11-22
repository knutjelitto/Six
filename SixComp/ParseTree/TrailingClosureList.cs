using SixComp.Support;
using System.Collections.Generic;
using System.Diagnostics;

namespace SixComp.ParseTree
{
    public class TrailingClosureList : ItemList<TrailingClosure>
    {
        public static readonly TokenSet Firsts = new TokenSet(ToKind.LBrace);

        public TrailingClosureList(List<TrailingClosure> closures) : base(closures) { }
        public TrailingClosureList() { }

        public bool BlockOnly => Count == 1 && this[0].BlockOnly;

        public CodeBlock ExtractBlock()
        {
            Debug.Assert(BlockOnly);

            var block = CodeBlock.From(this[0].Closure.Statements);
            ClearToMissing();
            return block;
        }

        public static TrailingClosureList Parse(Parser parser)
        {
            var closures = new List<TrailingClosure>();
            var first = true;

            do
            {
                var closure = TrailingClosure.TryParse(parser, first);
                if (closure == null)
                {
                    break;
                }
                closures.Add(closure);
                first = false;
            }
            while (parser.Current == ToKind.Name && parser.Next == ToKind.Colon && parser.Next == ToKind.LBrace);

            if (closures.Count == 0)
            {
                return new TrailingClosureList();
            }

            return new TrailingClosureList(closures);
        }

        public override string ToString()
        {
            return Missing ? string.Empty : " " + string.Join(" ", this);
        }
    }
}
