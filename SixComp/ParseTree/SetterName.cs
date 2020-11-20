using SixComp.Support;

namespace SixComp.ParseTree
{
    public class SetterName
    {
        public static readonly TokenSet Firsts = new TokenSet(ToKind.LParent);

        public SetterName(Name name)
        {
            Name = name;
        }

        public Name Name { get; }

        public static SetterName Parse(Parser parser)
        {
            parser.Consume(ToKind.LParent);
            var name = Name.Parse(parser);
            parser.Consume(ToKind.RParent);

            return new SetterName(name);
        }

        public override string ToString()
        {
            return $"({Name})";
        }
    }
}
