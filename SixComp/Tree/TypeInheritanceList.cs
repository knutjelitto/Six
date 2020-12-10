using System.Collections.Generic;

namespace SixComp
{
    public partial class ParseTree
    {
        public class TypeInheritanceList : ItemList<IType>
        {
            public TypeInheritanceList(List<IType> types) : base(types) { }
            public TypeInheritanceList() { }

            public static TypeInheritanceList Parse(Parser parser)
            {
                var types = new List<IType>();

                do
                {
                    var type = IType.Parse(parser);

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
}