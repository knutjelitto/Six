using SixComp.Support;
using System.Collections.Generic;

namespace SixComp
{
    public partial class Tree
    {
        public class AtTokenList : ItemList<AtToken>
        {
            public AtTokenList(List<AtToken> tokens) : base(tokens) { }
            public AtTokenList() { }

            public static AtTokenList Parse(Parser parser, ToKind right)
            {
                var tokens = new List<AtToken>();

                while (parser.Current != right)
                {
                    var token = (AtToken?)null;

                    switch (parser.Current)
                    {
                        case ToKind.LParent:
                            token = AtTokenGroup.Parse(parser, ToKind.LParent, ToKind.RParent);
                            break;
                        case ToKind.LBrace:
                            token = AtTokenGroup.Parse(parser, ToKind.LBrace, ToKind.RBrace);
                            break;
                        case ToKind.LBracket:
                            token = AtTokenGroup.Parse(parser, ToKind.LBracket, ToKind.RBracket);
                            break;
                        default:
                            if (parser.Current < ToKind._LAST_)
                            {
                                token = AtTokenSingle.Parse(parser);
                            }
                            break;
                    }

                    if (token == null)
                    {
                        throw new ParserException(parser.CurrentToken, "illformed attribute");
                    }

                    tokens.Add(token);
                }

                return new AtTokenList(tokens);
            }

            public override string ToString()
            {
                return string.Join(" ", this);
            }
        }
    }
}