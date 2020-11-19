namespace SixComp.ParseTree
{
    public class InitializerExpression : PostfixExpression
    {
        public InitializerExpression(AnyExpression left, ArgumentNameClause names) : base(left)
        {
            Names = names;
        }

        public ArgumentNameClause Names { get; }

        public static InitializerExpression Parse(Parser parser, AnyExpression left)
        {
            parser.Consume(ToKind.Dot);
            parser.Consume(ToKind.KwInit);
            var names = ArgumentNameClause.TryParse(parser) ?? new ArgumentNameClause();

            return new InitializerExpression(left, names);
        }
    }
}
