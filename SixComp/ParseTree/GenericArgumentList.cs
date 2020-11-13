using System.Collections.Generic;

namespace SixComp.ParseTree
{
    public class GenericArgumentList : ItemList<GenericArgument>
    {
        public GenericArgumentList(List<GenericArgument> items) : base(items) { }
        public GenericArgumentList() { }

        public static GenericArgumentList Parser(Parser parse)
        {

        }
    }
}
