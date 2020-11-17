using System.Collections.Generic;

namespace SixComp.ParseTree
{
    public class ParameterList : ItemList<Parameter>
    {
        public ParameterList(List<Parameter> items) : base(items) { }
        public ParameterList() { }

        public static ParameterList Parse(Parser parser)
        {
            parser.Consume(ToKind.LParent);

            var parameters = new List<Parameter>();

            while (parser.Current != ToKind.RParent)
            {
                var parameter = Parameter.Parse(parser);

                parameters.Add(parameter);

                if (!parser.Match(ToKind.Comma))
                {
                    break;
                }
            }
            parser.Consume(ToKind.RParent);

            return new ParameterList(parameters);
        }

        public override string ToString()
        {
            return "(" + string.Join(", ", this) + ")";
        }
    }
}
