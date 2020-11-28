using SixComp.Support;

namespace SixComp.ParseTree
{
    public class WhileStatement : AnyStatement
    {
        public WhileStatement(ConditionList condition, CodeBlock block)
        {
            Condition = condition;
            Block = block;
        }

        public ConditionList Condition { get; }
        public CodeBlock Block { get; }

        public static WhileStatement Parse(Parser parser)
        {
            parser.Consume(ToKind.KwWhile);
            var condition = ConditionList.Parse(parser);
            CodeBlock block;
            if (parser.Current != ToKind.LBrace && condition.LastExpression is FunctionCallExpression call && call.Closures.BlockOnly)
            {
                block = call.Closures.ExtractBlock();
            }
            else
            {
                block = CodeBlock.Parse(parser);
            }

            return new WhileStatement(condition, block);
        }

        public void Write(IWriter writer)
        {
            writer.WriteLine($"while {Condition}");
            Block.Write(writer);
        }

        public override string ToString()
        {
            return $"while {Condition} {Block}";
        }
    }
}
