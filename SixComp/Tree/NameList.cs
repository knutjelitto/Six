using System.Collections.Generic;

namespace SixComp.Tree
{
    public class NameList : ItemList<BaseName>
    {
        private NameList(List<BaseName> names) : base(names) { }
        public NameList() { }

        public static NameList Parse(Parser parser)
        {
            var names = new List<BaseName>();

            do
            {
                var name = BaseName.Parse(parser);
                names.Add(name);
            }
            while (parser.Match(ToKind.Comma));

            return new NameList(names);
        }

        public override string ToString()
        {
            return string.Join(", ", this);
        }
    }
}
