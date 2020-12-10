using SixComp.Support;

namespace SixComp
{
    public partial class ParseTree
    {
        public class SetterName
        {
            public static readonly TokenSet Firsts = new TokenSet(ToKind.LParent);

            public SetterName(BaseName name)
            {
                Name = name;
            }

            public BaseName Name { get; }

            public static SetterName Parse(Parser parser)
            {
                parser.Consume(ToKind.LParent);
                var name = BaseName.Parse(parser);
                parser.Consume(ToKind.RParent);

                return new SetterName(name);
            }

            public override string ToString()
            {
                return $"({Name})";
            }
        }
    }
}