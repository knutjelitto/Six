using SixComp.Support;

namespace SixComp.ParseTree
{
    public class IfStatement : AnyStatement
    {
        public IfStatement(AnyExpression condition, CodeBlock thenPart, CodeBlock? elsePart)
        {
            Condition = condition;
            ThenPart = thenPart;
            ElsePart = elsePart;
        }

        public AnyExpression Condition { get; }
        public CodeBlock ThenPart { get; }
        public CodeBlock? ElsePart { get; }

        public static IfStatement Parse(Parser parser)
        {
            parser.Consume(ToKind.KwIf);
            var condition = AnyExpression.Parse(parser);
            var thenPart = CodeBlock.Parse(parser);
            var elsePart = parser.TryMatch(ToKind.KwElse, CodeBlock.Parse);

            return new IfStatement(condition, thenPart, elsePart);
        }

        public void Write(IWriter writer)
        {
            writer.WriteLine($"if {Condition.StripParents()}");
            ThenPart.Write(writer);
            if (ElsePart != null)
            {
                writer.WriteLine("else");
                ElsePart.Write(writer);
            }
        }
    }
}
