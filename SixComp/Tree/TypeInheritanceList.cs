using System.Collections.Generic;

namespace SixComp.Tree
{
    public class TypeInheritanceList : ItemList<AnyType>
    {
        public TypeInheritanceList(List<AnyType> types) : base(types) { }
        public TypeInheritanceList() { }

        public static TypeInheritanceList Parse(Parser parser)
        {
            var types = new List<AnyType>();

            do
            {
                var type = AnyType.Parse(parser);

                types.Add(type);
            }
            while (parser.Match(ToKind.Comma));

            return new TypeInheritanceList(types);
        }

        public override string ToString()
        {
            return string.Join(", ", this);
        }
    }
}
