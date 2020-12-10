using System.Collections.Generic;

namespace SixComp
{
    public partial class ParseTree
    {
        public class GenericParameterList : ItemList<GenericParameter>
        {
            public GenericParameterList(List<GenericParameter> items) : base(items) { }
            public GenericParameterList() { }

            public static GenericParameterList Parse(Parser parser)
            {
                List<GenericParameter> names = new List<GenericParameter>();

                do
                {
                    var parameter = GenericParameter.Parse(parser);
                    names.Add(parameter);
                }
                while (parser.Match(ToKind.Comma));

                return new GenericParameterList(names);
            }

            public override string ToString()
            {
                return string.Join(", ", this);
            }
        }
    }
}