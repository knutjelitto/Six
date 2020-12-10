namespace SixComp
{
    public partial class ParseTree
    {
        public class ExplicitMemberExpression : PostfixExpression
        {
            public ExplicitMemberExpression(IExpression left, Token op)
                : base(left, op)
            {
            }

            public static ExplicitMemberExpression Parse(Parser parser, IExpression left)
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
                private TupleMemberSelector(IExpression left, Token op, ILiteralExpression.NumberLiteralExpression number)
                    : base(left, op)
                {
                    Number = number;
                }

                public ILiteralExpression.NumberLiteralExpression Number { get; }

                public static TupleMemberSelector Parse(Parser parser, IExpression left, Token op)
                {
                    //TODO: is incomplete - validate decimal digits
                    var number = ILiteralExpression.NumberLiteralExpression.Parse(parser);

                    return new TupleMemberSelector(left, op, number);
                }

                public override string ToString()
                {
                    return $"{Left}{Operator}{Number}";
                }
            }

            public class NamedMemberSelector : ExplicitMemberExpression
            {
                public NamedMemberSelector(IExpression left, Token op, FullName name, ArgumentNameClause names)
                    : base(left, op)
                {
                    Name = name;
                    Names = names;
                }

                public FullName Name { get; }
                public ArgumentNameClause Names { get; }

                public static NamedMemberSelector Parse(Parser parser, IExpression left, Token op)
                {
                    var name = FullName.Parse(parser);

                    var names = ArgumentNameClause.TryParse(parser) ?? new ArgumentNameClause();

                    return new NamedMemberSelector(left, op, name, names);
                }

                public override string ToString()
                {
                    return $"{Left}.{Name}{Names}";
                }
            }
        }
    }
}