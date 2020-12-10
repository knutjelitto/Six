namespace SixComp
{
    public partial class ParseTree
    {
        public class InitializerExpression : PostfixExpression
        {
            public InitializerExpression(IExpression left, Token op, ArgumentNameClause names)
                : base(left, op)
            {
                Names = names;
            }

            public ArgumentNameClause Names { get; }

            public static InitializerExpression Parse(Parser parser, IExpression left)
            {
                var op = parser.Consume(ToKind.Dot);
                parser.Consume(ToKind.KwInit);
                var names = ArgumentNameClause.TryParse(parser) ?? new ArgumentNameClause();

                return new InitializerExpression(left, op, names);
            }
        }
    }
}