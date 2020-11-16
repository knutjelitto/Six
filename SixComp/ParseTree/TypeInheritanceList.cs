using System.Collections.Generic;

namespace SixComp.ParseTree
{
    public class TypeInheritanceList : ItemList<TypeIdentifier>
    {
        public TypeInheritanceList(List<TypeIdentifier> types) : base(types) { }
        public TypeInheritanceList() { }

        public static TypeInheritanceList Parse(Parser parser)
        {
            var types = new List<TypeIdentifier>();

            do
            {
                var type = TypeIdentifier.Parse(parser);

                types.Add(type);
            }
            while (parser.Match(ToKind.Comma));

            return new TypeInheritanceList(types);
        }
    }
}
