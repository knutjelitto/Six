using System.Collections.Generic;

namespace SixComp.ParseTree
{
    public class ParameterList : ItemList<Parameter>
    {
        public ParameterList(List<Parameter> items) : base(items) { }
        public ParameterList() { }

        public override string ToString()
        {
            return string.Join(", ", this);
        }
    }
}
