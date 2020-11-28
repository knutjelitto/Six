namespace SixComp.ParseTree
{
    public class KeyPathComponent
    {
        public KeyPathComponent(Name? name, KeyPathPostfixList postfixes)
        {
            Name = name;
            Postfixes = postfixes;
        }

        public Name? Name { get; }
        public KeyPathPostfixList Postfixes { get; }

        public static KeyPathComponent Parse(Parser parser)
        {
            var name = parser.Current == ToKind.Name
                ? Name.Parse(parser)
                : null
                ;

            var postfixes = KeyPathPostfixList.Parse(parser);

            return new KeyPathComponent(name, postfixes);
        }

        public override string ToString()
        {
            return $"{Name}{Postfixes}";
        }
    }
}
