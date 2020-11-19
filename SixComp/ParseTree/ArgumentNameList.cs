using SixComp.Support;
using System.Collections.Generic;

namespace SixComp.ParseTree
{
    public class ArgumentNameList : ItemList<ArgumentName>
    {
        public ArgumentNameList(List<ArgumentName> names) : base(names) { }
        public ArgumentNameList() { }

        public static ArgumentNameList? TryParse(Parser parser, TokenSet follow)
        {
            var names = new List<ArgumentName>();

            do
            {
                var name = ArgumentName.TryParse(parser);
                if (name == null)
                {
                    return null;
                }
                names.Add(name);
            }
            while (!follow.Contains(parser.Current));

            return new ArgumentNameList(names);

        }
    }
}
