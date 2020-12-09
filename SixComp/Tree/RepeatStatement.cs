using System;

namespace SixComp
{
    public partial class Tree
    {
        public class RepeatStatement : AnyStatement
        {
            public RepeatStatement(CodeBlock block, AnyExpression condition)
            {
                Block = block;
                Condition = condition;
            }

            public CodeBlock Block { get; }
            public AnyExpression Condition { get; }

            public static RepeatStatement Parse(Parser parser)
            {
                parser.Consume(ToKind.KwRepeat);
                var block = CodeBlock.Parse(parser);
                parser.Consume(ToKind.KwWhile);
                var condition = AnyExpression.TryParse(parser) ?? throw new InvalidOperationException($"{typeof(RepeatStatement)}");

                return new RepeatStatement(block, condition);
            }

            public override string ToString()
            {
                return $"{Kw.Repeat} {Block} {Kw.While} {Condition}";
            }
        }
    }
}