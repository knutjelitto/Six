using System.Collections.Generic;

namespace SixComp.ParseTree
{
    public class ClosureParameterList : ItemList<ClosureParameter>
    {
        public ClosureParameterList(List<ClosureParameter> parameters) : base(parameters) { }
        public ClosureParameterList() { }

        public bool NameOnly => Count == 1 && this[0].NameOnly;

        public static ClosureParameterList Parse(Parser parser, bool nameOnly)
        {
            var parameters = new List<ClosureParameter>();

            if (parser.Current != ToKind.RParent || nameOnly/*can't be empty*/)
            {
                do
                {
                    var parameter = ClosureParameter.Parse(parser, nameOnly);
                    parameters.Add(parameter);
                }
                while (parser.Match(ToKind.Comma));
            }

            return new ClosureParameterList(parameters);
        }
    }
}
