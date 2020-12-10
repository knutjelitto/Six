using System.Collections.Generic;

namespace SixComp
{
    public partial class ParseTree
    {
        public class KeyPathPostfixList : ItemList<IKeyPathPostfix>
        {
            public KeyPathPostfixList(List<IKeyPathPostfix> postfixes) : base(postfixes) { }
            public KeyPathPostfixList() { }

            public static KeyPathPostfixList Parse(Parser parser)
            {
                if (IKeyPathPostfix.Firsts.Contains(parser.Current))
                {
                    var postfixes = new List<IKeyPathPostfix>();

                    do
                    {
                        var postfix = IKeyPathPostfix.Parse(parser);
                        postfixes.Add(postfix);
                    }
                    while (IKeyPathPostfix.Firsts.Contains(parser.Current));

                    return new KeyPathPostfixList(postfixes);
                }

                return new KeyPathPostfixList();
            }

            public override string ToString()
            {
                return string.Join(string.Empty, this);
            }
        }
    }
}