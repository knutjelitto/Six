namespace SixComp.ParseTree
{
    public class ExplicitMemberExpression : PostfixExpression
    {
        public ExplicitMemberExpression(AnyExpression left, Token op) : base(left, op)
        {
        }

        public static ExplicitMemberExpression Parse(Parser parser, AnyExpression left)
        {
            var op = parser.Consume(ToKind.Dot);

            if (parser.Current == ToKind.Number)
            {
                return TupleMemberSelector.Parse(parser, left, op);
            }

            return NamedMemberSelector.Parse(parser, left, op);
        }

        public class TupleMemberSelector : ExplicitMemberExpression
        {
            private TupleMemberSelector(AnyExpression left, Token op, NumberLiteralExpression number)
                : base(left, op)
            {
                Number = number;
            }

            public NumberLiteralExpression Number { get; }

            public static TupleMemberSelector Parse(Parser parser, AnyExpression left, Token op)
            {
                //TODO: is incomplete - validate decimal digits
                var number = NumberLiteralExpression.Parse(parser);

                return new TupleMemberSelector(left, op, number);
            }
        }

        public class NamedMemberSelector : ExplicitMemberExpression
        {
            public NamedMemberSelector(AnyExpression left, Token op, Name name, GenericArgumentClause generics, ArgumentNameClause names)
                : base(left, op)
            {
                Name = name;
                Generics = generics;
                Names = names;
            }

            public Name Name { get; }
            public GenericArgumentClause Generics { get; }
            public ArgumentNameClause Names { get; }

            public static NamedMemberSelector Parse(Parser parser, AnyExpression left, Token op)
            {
                var name = Name.Parse(parser);

                var generics = parser.Adjacent
                    ? parser.TryList(ToKind.Less, GenericArgumentClause.Parse)
                    : new GenericArgumentClause();
                var names = ArgumentNameClause.TryParse(parser) ?? new ArgumentNameClause();

                return new NamedMemberSelector(left, op, name, generics, names);
            }

            public override string ToString()
            {
                return $"{Left}.{Name}{Generics}{Names}";
            }
        }
    }
}
