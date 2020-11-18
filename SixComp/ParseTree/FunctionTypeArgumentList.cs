using System.Collections.Generic;

namespace SixComp.ParseTree
{
    public class FunctionTypeArgumentList : ItemList<FunctionTypeArgument>
    {
        public FunctionTypeArgumentList(List<FunctionTypeArgument> arguments) : base(arguments) { }
        public FunctionTypeArgumentList() { }

        public static FunctionTypeArgumentList Parse(Parser parser)
        {
            var arguments = new List<FunctionTypeArgument>();

            do
            {
                var argument = FunctionTypeArgument.Parse(parser);
                arguments.Add(argument);
            }
            while (parser.Match(ToKind.Comma));

            return new FunctionTypeArgumentList(arguments);
        }
    }
}
