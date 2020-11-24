namespace SixComp.ParseTree
{
    public class InOutExpression : BaseExpression, AnyPrefixExpression
    {
        private InOutExpression(Name name)
        {
            Name = name;
        }

        public Name Name { get; }

        public static InOutExpression? TryParse(Parser parser)
        {
            var offset = parser.Offset;

            if (parser.Match(ToKind.Amper) )
            {
                var name = Name.TryParse(parser);

                if (name != null)
                {
                    return new InOutExpression(name);
                }
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
