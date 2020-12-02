using SixComp.Support;
using System.Diagnostics;

namespace SixComp.Tree
{
    public class ArgumentName
    {
        private static readonly TokenSet Deny = new TokenSet(ToKind.KwInout, ToKind.KwVar, ToKind.KwLet);

        public ArgumentName(BaseName name)
        {
            Name = name;
        }

        public BaseName Name { get; }

        public static ArgumentName? TryParse(Parser parser)
        {
            if (parser.Next == ToKind.Colon && (parser.Current == ToKind.Name || (parser.IsKeyword && !Deny.Contains(parser.Current))))
            {
                if (parser.Current != ToKind.Name)
                {
                    Debug.Assert(true);
                }
                var name = parser.ConsumeAny();
                parser.Consume(ToKind.Colon);

                return new ArgumentName(BaseName.From(name));
            }

            return null;
        }

        public override string ToString()
        {
            return $"{Name}:";
        }
    }
}
