namespace SixComp.ParseTree
{
    public class GuardStatement : AnyStatement
    {
        public GuardStatement(ConditionList conditions, CodeBlock block)
        {
            Conditions = conditions;
        }

        public ConditionList Conditions { get; }

        public static GuardStatement Parse(Parser parser)
        {
            parser.Consume(ToKind.KwGuard);
            var conditions = ConditionList.Parse(parser);
            parser.Consume(ToKind.KwElse);
            var block = CodeBlock.Parse(parser);

            return new GuardStatement(conditions, block);
        }
    }
}
