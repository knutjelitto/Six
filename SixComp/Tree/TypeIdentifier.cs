using System.Collections.Generic;

namespace SixComp
{
    public partial class ParseTree
    {
        public class TypeIdentifier : ItemList<FullName>, IType
        {
            public TypeIdentifier(List<FullName> names) : base(names) { }

            public static TypeIdentifier Parse(Parser parser)
            {
                var names = new List<FullName>();

                do
                {
                    var name = FullName.Parse(parser);
                    names.Add(name);
                }
                while (parser.Match(ToKind.Dot));

                return new TypeIdentifier(names);
            }

            public override string ToString()
            {
                return string.Join(".", this);
            }
        }
    }
}