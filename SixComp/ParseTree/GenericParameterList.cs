using System;
using System.Collections.Generic;
using System.Text;

namespace SixComp.ParseTree
{
    public class GenericParameterList : ItemList<GenericParameter>
    {
        public GenericParameterList(List<GenericParameter> items) : base(items) { }
        public GenericParameterList() { }
    }
}
