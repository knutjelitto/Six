using System.Collections.Generic;

namespace SixComp.ParseTree
{
    public class NameList : ItemList<Name>
    {
        private NameList(List<Name> names) : base(names) { }
        public NameList() { }

        public static NameList Parse(Parser parser)
        {
            var names = new List<Name>();

            do
            {
                var name = Name.Parse(parser);
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
