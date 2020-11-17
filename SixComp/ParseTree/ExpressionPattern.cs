namespace SixComp.ParseTree
{
    public class ExpressionPattern : AnyPattern, AnyPrimary
    {
        private ExpressionPattern(AnyExpression? expression, AnyPattern? pattern)
        {
            Expression = expression;
            Pattern = pattern;
        }

        public AnyExpression? Expression { get; }
        public AnyPattern? Pattern { get; }

        public static ExpressionPattern Parse(Parser parser)
        {
            switch (parser.Current.Kind)
            {
                case ToKind.KwLet:
                    return new ExpressionPattern(null, LetPattern.Parse(parser));
                case ToKind.KwVar:
                    return new ExpressionPattern(null, VarPattern.Parse(parser));
            }

            var expression = AnyExpression.Parse(parser);

            return new ExpressionPattern(expression, null);
        }
    }
}
