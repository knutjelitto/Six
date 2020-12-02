using System.Collections.Generic;

namespace SixComp.Tree
{
    public class KeyPathPostfixList : ItemList<AnyKeyPathPostfix>
    {
        public KeyPathPostfixList(List<AnyKeyPathPostfix> postfixes) : base(postfixes) { }
        public KeyPathPostfixList() { }

        public static KeyPathPostfixList Parse(Parser parser)
        {
            if (AnyKeyPathPostfix.Firsts.Contains(parser.Current))
            {
                var postfixes = new List<AnyKeyPathPostfix>();

                do
                {
                    var postfix = AnyKeyPathPostfix.Parse(parser);
                    postfixes.Add(postfix);
                }
                while (AnyKeyPathPostfix.Firsts.Contains(parser.Current));
            
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
