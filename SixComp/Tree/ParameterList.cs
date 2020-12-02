using SixComp.Support;
using System.Collections.Generic;

namespace SixComp.Tree
{
    public class ParameterList : ItemList<Parameter>
    {
        public ParameterList(List<Parameter> items) : base(items) { }
        public ParameterList() { }

        public static ParameterList Parse(Parser parser, TokenSet follow)
        {
            var parameters = new List<Parameter>();

            while (!follow.Contains(parser.Current))
            {
                var parameter = Parameter.Parse(parser);

                parameters.Add(parameter);

                if (!parser.Match(ToKind.Comma))
                {
                    break;
                }
            }

            return new ParameterList(parameters);
        }

        public override string ToString()
        {
            return string.Join(", ", this);
        }
    }
}
