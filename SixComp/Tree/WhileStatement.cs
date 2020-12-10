using SixComp.Support;

namespace SixComp
{
    public partial class ParseTree
    {
        public class WhileStatement : IStatement
        {
            public WhileStatement(ConditionList conditions, CodeBlock block)
            {
                Conditions = conditions;
                Block = block;
            }

            public ConditionList Conditions { get; }
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
                writer.WriteLine($"while {Conditions}");
                Block.Write(writer);
            }

            public override string ToString()
            {
                return $"while {Conditions} {Block}";
            }
        }
    }
}