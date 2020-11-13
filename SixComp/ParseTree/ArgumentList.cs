using System.Collections.Generic;

namespace SixComp.ParseTree
{
    public class ArgumentList : ItemList<Argument>
    {
        public ArgumentList(List<Argument> arguments) : base(arguments) { }
        public ArgumentList() { }
    }
}
