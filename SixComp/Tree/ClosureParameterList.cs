using System.Collections.Generic;

namespace SixComp
{
    public partial class ParseTree
    {
        public class ClosureParameterList : ItemList<ClosureParameter>
        {
            public ClosureParameterList(List<ClosureParameter> parameters) : base(parameters) { }
            public ClosureParameterList() { }

            public bool OneNameOnly => Count == 1 && this[0].NameOnly;

            public static ClosureParameterList? TryParse(Parser parser, bool nameOnly)
            {
                var parameters = new List<ClosureParameter>();

                if (parser.Current == ToKind.RParent)
                {
                    return new ClosureParameterList(parameters);
                }

                var offset = parser.Offset;

                do
                {
                    var parameter = ClosureParameter.TryParse(parser, nameOnly);
                    if (parameter == null)
                    {
                        parser.Offset = offset;
                        return null;
                    }
                    parameters.Add(parameter);
                }
                while (parser.Match(ToKind.Comma));

                return new ClosureParameterList(parameters);
            }

            public override string ToString()
            {
                return string.Join(", ", this);
            }
        }
    }
}