using System;

namespace SixComp.ParseTree
{
    public class RepeatStatement : AnyStatement
    {
        public RepeatStatement(CodeBlock block, AnyExpression condition)
        {
            Condition = condition;
            Block = block;
        }

        public AnyExpression Condition { get; }
        public CodeBlock Block { get; }

        public static RepeatStatement Parse(Parser parser)
        {
            parser.Consume(ToKind.KwRepeat);
            var block = CodeBlock.Parse(parser);
            parser.Consume(ToKind.KwWhile);
            var condition = AnyExpression.TryParse(parser) ?? throw new InvalidOperationException($"{typeof(RepeatStatement)}");

            return new RepeatStatement(block, condition);
        }
    }
}
