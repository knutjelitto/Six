﻿using SixComp.Support;
using System.Collections.Generic;

namespace SixComp
{
    public partial class ParseTree
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

            public override string ToString()
            {
                return string.Join(string.Empty, this);
            }
        }
    }
}