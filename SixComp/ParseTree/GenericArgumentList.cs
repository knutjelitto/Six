using System.Collections.Generic;

namespace SixComp.ParseTree
{
    public class GenericArgumentList : ItemList<GenericArgument>
    {
        public GenericArgumentList(List<GenericArgument> items) : base(items) { }
        public GenericArgumentList() { }

        public static GenericArgumentList Parse(Parser parser)
        {
            var arguments = new List<GenericArgument>();

            do
            {
                var argument = GenericArgument.Parse(parser);
                arguments.Add(argument);
            }
            while (parser.Match(ToKind.Comma));

            return new GenericArgumentList(arguments);
        }

        public override string ToString()
        {
            return string.Join(", ", this);
        }
    }
}
