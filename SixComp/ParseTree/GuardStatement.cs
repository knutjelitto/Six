using SixComp.Support;

namespace SixComp.ParseTree
{
    public class GuardStatement : AnyStatement
    {
        public GuardStatement(ConditionList conditions, CodeBlock block)
        {
            Conditions = conditions;
            Block = block;
        }

        public ConditionList Conditions { get; }
        public CodeBlock Block { get; }

        public static GuardStatement Parse(Parser parser)
        {
            parser.Consume(ToKind.KwGuard);
            var conditions = ConditionList.Parse(parser);
            parser.Consume(ToKind.KwElse);
            var block = CodeBlock.Parse(parser);

            return new GuardStatement(conditions, block);
        }

        public void Write(IWriter writer)
        {
            var block = $"{Block}";

            if (block.Length <= 100)
            {
                writer.WriteLine($"guard {Conditions} else {block}");
            }
            else
            {
                writer.WriteLine($"guard {Conditions} else");
                Block.Write(writer);
            }
        }

        public override string ToString()
        {
            return $"guard {Conditions} else {Block}";
        }
    }
}
