namespace SixComp.ParseTree
{
    public class InOutExpression : BaseExpression, AnyPrefix
    {
        public InOutExpression(Name name)
        {
            Name = name;
        }

        public Name Name { get; }

        public static InOutExpression? TryParse(Parser parser)
        {
            var offset = parser.Offset;

            if (parser.Match(ToKind.Amper) && parser.Current == ToKind.Name)
            {
                var name = Name.Parse(parser);

                return new InOutExpression(name);
            }

            parser.Offset = offset;
            return null;
        }

        public override string ToString()
        {
            return $"&{Name}";
        }
    }
}
