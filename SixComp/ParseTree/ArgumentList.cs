using SixComp.Support;
using System.Collections.Generic;
using System.Linq;

namespace SixComp.ParseTree
{
    public class ArgumentList : ItemList<Argument>
    {
        public ArgumentList(List<Argument> arguments) : base(arguments) { }
        public ArgumentList() { }

        public static ArgumentList Parse(Parser parser)
        {
            var arguments = new List<Argument>();

            if (parser.Current != ToKind.RParent)
            {
                do
                {
                    var argument = Argument.Parse(parser);
                    arguments.Add(argument);
                }
                while (parser.Match(ToKind.Comma));
            }

            return new ArgumentList(arguments);
        }

        public override string ToString()
        {
            return string.Join(", ", this.Select(a => a.StripParents()));
        }
    }
}
