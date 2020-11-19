namespace SixComp.ParseTree
{
    public class FunctionTypeArgumentClause
    {
        private FunctionTypeArgumentClause(LabeledTypeList arguments, bool variadic)
        {
            Arguments = arguments;
            Variadic = variadic;
        }

        public LabeledTypeList Arguments { get; }
        public bool Variadic { get; }

        public static FunctionTypeArgumentClause Parse(Parser parser)
        {
            parser.Consume(ToKind.LParent);

            var arguments = LabeledTypeList.Parse(parser);
            var variadic = parser.Match(ToKind.DotDotDot);

            parser.Consume(ToKind.RParent);

            return new FunctionTypeArgumentClause(arguments, variadic);
        }

        public static FunctionTypeArgumentClause From(LabeledTypeList arguments)
        {
            return new FunctionTypeArgumentClause(arguments, false);
        }
    }
}
