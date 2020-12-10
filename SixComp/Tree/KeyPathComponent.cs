namespace SixComp
{
    public partial class ParseTree
    {
        public class KeyPathComponent
        {
            public KeyPathComponent(BaseName? name, KeyPathPostfixList postfixes)
            {
                Name = name;
                Postfixes = postfixes;
            }

            public BaseName? Name { get; }
            public KeyPathPostfixList Postfixes { get; }

            public static KeyPathComponent Parse(Parser parser)
            {
                var name = parser.Current == ToKind.Name
                    ? BaseName.Parse(parser)
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
}