using System;

namespace SixComp
{
    public partial class ParseTree
    {
        public class RepeatStatement : IStatement
        {
            public RepeatStatement(CodeBlock block, IExpression condition)
            {
                Block = block;
                Condition = condition;
            }

            public CodeBlock Block { get; }
            public IExpression Condition { get; }

            public static RepeatStatement Parse(Parser parser)
            {
                parser.Consume(ToKind.KwRepeat);
                var block = CodeBlock.Parse(parser);
                parser.Consume(ToKind.KwWhile);
                var condition = IExpression.TryParse(parser) ?? throw new InvalidOperationException($"{typeof(RepeatStatement)}");

                return new RepeatStatement(block, condition);
            }

            public override string ToString()
            {
                return $"{Kw.Repeat} {Block} {Kw.While} {Condition}";
            }
        }
    }
}