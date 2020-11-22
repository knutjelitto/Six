using System.Collections.Generic;

namespace SixComp.ParseTree
{

    public class TypeIdentifier : ItemList<TypeName>, AnyType
    {
        public TypeIdentifier(List<TypeName> names) : base(names) { }

        public static TypeIdentifier Parse(Parser parser)
        {
            var names = new List<TypeName>();

            do
            {
                var name = TypeName.Parse(parser);
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
