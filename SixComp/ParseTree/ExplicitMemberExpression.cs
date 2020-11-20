namespace SixComp.ParseTree
{
    public class ExplicitMemberExpression : PostfixExpression
    {
        public ExplicitMemberExpression(AnyExpression left) : base(left)
        {
        }

        public static ExplicitMemberExpression Parse(Parser parser, AnyExpression left)
        {
            parser.Consume(ToKind.Dot);

            if (parser.Current == ToKind.Number)
            {
                return TupleMemberSelector.Parse(parser, left);
            }

            return NamedMemberSelector.Parse(parser, left);
        }

        public class TupleMemberSelector : ExplicitMemberExpression
        {
            private TupleMemberSelector(AnyExpression left, NumberLiteralExpression number)
                : base(left)
            {
                Number = number;
            }

            public NumberLiteralExpression Number { get; }

            public static new TupleMemberSelector Parse(Parser parser, AnyExpression left)
            {
                //TODO: validate decimal digits
                var number = NumberLiteralExpression.Parse(parser);

                return new TupleMemberSelector(left, number);
            }
        }

        public class NamedMemberSelector : ExplicitMemberExpression
        {
            public NamedMemberSelector(AnyExpression left, Name name, GenericArgumentClause generics, ArgumentNameClause names)
                : base(left)
            {
                Name = name;
                Generics = generics;
                Names = names;
            }

            public Name Name { get; }
            public GenericArgumentClause Generics { get; }
            public ArgumentNameClause Names { get; }

            public static new NamedMemberSelector Parse(Parser parser, AnyExpression left)
            {
                var name = Name.Parse(parser);

                var generics = parser.Adjacent
                    ? parser.TryList(ToKind.Less, GenericArgumentClause.Parse)
                    : new GenericArgumentClause();
                var names = ArgumentNameClause.TryParse(parser) ?? new ArgumentNameClause();

                return new NamedMemberSelector(left, name, generics, names);
            }

            public override string ToString()
            {
                return $"{Left}.{Name}{Generics}{Names}";
            }
        }
    }
}
